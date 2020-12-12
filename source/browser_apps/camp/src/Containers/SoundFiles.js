import "./SoundFiles.css";
import React, {Component} from "react";
import cAmpService from "../Services/cAmpService";
import {Row, Col, Table} from "react-bootstrap";
import {Link} from 'react-router-dom'
import {toast} from "react-toastify";
import SongPlay from "../Components/SongPlay";
import SongStop from "../Components/SongStop";
import {Howl} from 'howler';
import AddToButton from "../Components/AddToButton";
import Favorite from "../Components/Favorite";

export default class SoundFiles extends Component {
  constructor(props) {
    super(props);

    let albumId = this.props.match.params.albumId;
    if (albumId === undefined) {
      albumId = null;
    }

    let artistId = this.props.match.params.artistId;
    if (artistId === undefined) {
      artistId = null;
    }

    let playListId = this.props.match.params.playListId;
    if (playListId === undefined) {
      playListId = null;
    }

    this.state = {
      artistId: artistId,
      albumId: albumId,
      playListId: playListId,
      soundFiles: [],
      currentSoundFilePosition: 0,
      currentSoundFile: null,
      blob: null,
      howl: null
    };

    this.renderMediaButton = this.renderMediaButton.bind(this);
    this.handlePlay = this.handlePlay.bind(this);
    this.handlePlayNext = this.handlePlayNext.bind(this);
    this.handleStop = this.handleStop.bind(this);
    this.findSoundFile = this.findSoundFile.bind(this);
    this.songFinished = this.songFinished.bind(this);
    this.getFile = this.getFile.bind(this);
    this.handleQueueModified = this.handleQueueModified.bind(this);
    this.handleToggleIsFavorite = this.handleToggleIsFavorite.bind(this);
  }

  componentDidMount() {
    this.loadSongs();
  }

  componentDidUpdate(prevProps, prevState, snapshot) {
    if (prevProps.queuePlayerPlaying === false
        && this.props.queuePlayerPlaying === true) {
      if (this.state.currentSoundFile !== undefined
          && this.state.currentSoundFile !== null) {
        this.handleStop(this.state.currentSoundFile.id);
      }
    }
  }

  componentWillUnmount() {
    let howl = this.state.howl;
    if (howl !== null) {
      howl.stop();
      howl.unload();

      cAmpService.setSoundFileComplete(this.state.currentSoundFile.id, false)
        .then(r => {
          //Not sure if critical
        })
        .catch(error => {
          alert(error.message);
        });
    }
  }

  loadSongs() {
    if (this.state.artistId !== null) {
      cAmpService.getSoundFilesByArtist(this.state.artistId)
        .then(r => {
          this.setState({
            soundFiles: r
          });
        })
        .catch(e => {
          toast.error(e.message);
        });
    } else if (this.state.albumId !== null) {
      cAmpService.getSoundFilesByAlbum(this.state.albumId)
        .then(r => {
          this.setState({
            soundFiles: r
          });
        })
        .catch(e => {
          toast.error(e.message);
        });
    } else if (this.state.playListId !== null) {
      cAmpService.getPlayListSoundFiles(this.state.playListId)
        .then(r => {
          this.setState({
            soundFiles: r
          });
        })
        .catch(e => {
          toast.error(e.message);
        });
    } else {
      cAmpService.getSoundFiles()
        .then(r => {
          this.setState({
            soundFiles: r
          });
        })
        .catch(e => {
          toast.error(e.message);
        });
    }
  }

  handlePlayNext() {
    let countOfSongs = this.state.soundFiles.length;
    let currentPosition = this.state.currentSoundFilePosition;
    let nextPosition = currentPosition += 1;

    if (nextPosition < countOfSongs) {
      let soundFile = this.state.soundFiles[nextPosition];

      this.setState({
        currentSoundFilePosition: nextPosition,
        currentSoundFile: soundFile
      }, () => {
        this.getFile(soundFile.id);
      });
    }
  }

  findSoundFile(soundFileId) {
    let soundFiles = this.state.soundFiles;

    for (let i = 0; i < soundFiles.length; i++) {
      let sf = soundFiles[i];

      if (sf.id === soundFileId) {
        return {position: i, soundFile: sf};
      }
    }

    return null;
  }

  getFile(soundFileId) {
    cAmpService.getSoundFile(soundFileId)
      .then(r => {
        this.setState({
          blob: r
        }, () => {
          this.playBlob();
        })
      })
      .catch(error => {
        alert(error.message);
      });
  }

  handlePlay(soundFileId) {
    if (this.props.queuePlayerShouldStop !== undefined
        && this.props.queuePlayerShouldStop !== null) {
      this.props.queuePlayerShouldStop();
    }

    let results = this.findSoundFile(soundFileId);

    if (results !== null) {
      this.setState({
        currentSoundFilePosition: results.position,
        currentSoundFile: results.soundFile
      }, () => {
        this.getFile(results.soundFile.id);
      });
    }
  }

  songFinished() {
    let howl = this.state.howl;
    if (howl !== null) {
      howl.unload();

      cAmpService.setSoundFileComplete(this.state.currentSoundFile.id, true)
        .then(r => {
          //Not sure if critical
        })
        .catch(error => {
          alert(error.message);
        });

      this.setState({
        howl: null,
        blob: null,
        currentSoundFile: null
      }, () => {
        this.handlePlayNext();
      });
    } else {
      this.handlePlayNext();
    }
  }

  playBlob() {
    if (this.state.howl !== null) {
      if (this.state.howl.playing()) {
        this.state.howl.stop();
      }
    }
    let url = URL.createObjectURL(this.state.blob);

    let howl = new Howl({
      src: [url],
      format: ['mp3'],
    });

    howl.once('end', this.songFinished);

    this.setState({
      howl: howl
    }, () => {
      this.state.howl.play();
    });
  }

  handleStop(soundFileId) {
    if (this.state.howl != null) {
      this.state.howl.stop();

      cAmpService.setSoundFileComplete(this.state.currentSoundFile.id, false)
        .then(r => {

        })
        .catch(e => {
          //Do nothing.  Not critical
        });
    }

    this.setState({
      howl: null,
      blob: null,
      currentSoundFile: null
    });
  }

  handleQueueModified() {
    this.props.queuePlayerShouldRefresh();
  }

  renderMediaButton(soundFileId) {
    if (this.state.currentSoundFile !== undefined
      && this.state.currentSoundFile !== null) {
      if (this.state.currentSoundFile.id === soundFileId) {
        return <SongStop soundFileId={soundFileId} onStop={this.handleStop}/>;
      }
    }

    return <SongPlay soundFileId={soundFileId} onPlay={this.handlePlay}/>;
  }

  handleToggleIsFavorite(soundFileId) {
    cAmpService.toggleSoundFileFavorite(soundFileId)
      .then(r => {

      })
      .catch(error => {
        toast.error(error.message);
      })
  }

  renderIsFavorite(soundFile) {
    return <Favorite soundFileId={soundFile.id} isFavorite={false} onClick={this.handleToggleIsFavorite} />;
  }

  renderTableBody(list) {
    if (list != null
      && list.length > 0) {
      let rows = list.map((item, i) => {

        if (item.id != null) {
          let artistUrl = `/artists/${item.artistId}`;
          let albumsUrl = `/albums/${item.albumId}`;

          return (
            <tr key={item.id}>
              <td>{this.renderIsFavorite(item)}</td>
              <td>{this.renderMediaButton(item.id)}</td>
              <td>{item.title}</td>
              <td><Link to={artistUrl}>{item.artist}</Link></td>
              <td><Link to={albumsUrl}>{item.album}</Link></td>
              <td>
                <AddToButton
                  id={item.id}
                  type="soundfile"
                  onQueueModified={this.handleQueueModified}/>
              </td>
            </tr>
          );
        } else {
          return "";
        }
      });

      return (
        <tbody>
        {rows}
        </tbody>);
    }

    return (
      <tbody></tbody>);
  }

  render() {
    return (
      <div className="SoundFiles">
        <Row>
          <Col>
            <h3>Songs</h3>
          </Col>
          <Col>
          </Col>
        </Row>
        <Row>
          <Col>
            <Table striped bordered hover>
              <thead>
              <tr>
                <th>Favorite</th>
                <th>Play</th>
                <th>Name</th>
                <th>Artist</th>
                <th>Album</th>
                <th>Action</th>
              </tr>
              </thead>
              {this.renderTableBody(this.state.soundFiles)}
            </Table>
          </Col>
        </Row>
      </div>
    );
  }
}