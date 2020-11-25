import "./QueuePlayer.css";
import React, { Component } from "react";
import {Row, Col} from "react-bootstrap";
import cAmpService from "../Services/cAmpService";
import {toast} from "react-toastify";
import Play from "./Play";
import Next from "./Next";
import Stop from "./Stop";
import {Howl} from "howler";

export default class QueuePlayer extends Component {

  constructor(props) {
    super(props);

    this.state = {
      username: "",
      playing: false,
      shouldStop: false,
      queueSize: 0,
      currentSoundFile: null,
      currentSoundFileName: null,
      nextSoundFile: null,
      nextSoundFileName: null,
      blob: null,
      howl: null
    };

    this.handleShouldStop = this.handleShouldStop.bind(this);
    this.handleShouldRefresh = this.handleShouldRefresh.bind(this);

    this.refresh = this.refresh.bind(this);

    this.handlePlay = this.handlePlay.bind(this);
    this.handleStop = this.handleStop.bind(this);
    this.handleNext = this.handleNext.bind(this);

    this.getFile = this.getFile.bind(this);
    this.playBlob = this.playBlob.bind(this);
    this.songFinished = this.songFinished.bind(this);
  }

  componentDidMount() {
    this.refresh();
  }

  componentDidUpdate(prevProps, prevState, snapshot) {
    if (prevProps.shouldStop !== this.props.shouldStop
        && this.props.shouldStop === true) {
      this.setState({
        shouldStop: true
      }, ()=> {
        this.handleShouldStop();
      });
    }

    if (prevProps.shouldRefresh !== this.props.shouldRefresh
        && this.props.shouldRefresh === true) {
      this.handleShouldRefresh();
    }
  }

  handleShouldStop(){
    this.handleStop()
    this.props.playStopped();
  }

  refresh() {
    cAmpService.getQueueSize()
      .then(r => {
        this.setState({
          queueSize: r
        }, () => {
          this.props.refreshed();
        });
      })
      .catch(e => {
        toast.error(e.message);
      });

    cAmpService.getCurrentQueueSong()
      .then(r => {
        this.setState({
          currentSoundFile: r,
          currentSoundFileName: r.title
        })
      })
      .catch(e => {
        toast.error(e.message);
      });

    cAmpService.getNextQueueSong()
      .then(r => {
        this.setState({
          nextSoundFile: r,
          nextSoundFileName: r.title
        })
      })
      .catch(e => {
        toast.error(e.message);
      });
  }

  handleShouldRefresh() {
    this.refresh();
  }

  handleNext() {
    cAmpService.advanceToNextQueueSong()
      .then(r => {
        if (r != null) {
          this.setState({
            currentSoundFile: r,
            currentSoundFileName: r.title
          }, () => {
            this.getFile(this.state.currentSoundFile.id);

            cAmpService.getQueueSize()
              .then(r => {
                this.setState({
                  queueSize: r
                }, () => {
                  this.props.refreshed();
                });
              })
              .catch(e => {
                toast.error(e.message);
              });

            cAmpService.getNextQueueSong()
              .then(r => {
                if (r!=null) {
                  this.setState({
                    nextSoundFile: r
                  })
                }
              })
              .catch(e => {
                toast.error(e.message);
              });
          });
        }
      });
  }

  handleStop() {
    if (this.state.playing) {
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
        playing: false
      }, ()=> {
        this.props.playStopped();
      });
    }
  }

  handlePlay() {
    if (!this.state.playing
        && this.state.currentSoundFile !== null) {
      this.setState({
        playing: true
      }, ()=> {
        this.props.playStarted();

        this.getFile(this.state.currentSoundFile.id);
      });
    }
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
        this.handleNext();
      });
    } else {
      this.handleNext();
    }
  }

  render() {
    return (
      <div className="QueuePlayer">
        <Row>
          <Col>
            Queue Player
          </Col>
          <Col>
            Queue Size: {this.state.queueSize}
          </Col>
          <Col>
            Playing: {this.state.playing.toString()}
          </Col>
          <Col>
            Should Stop: {this.props.shouldStop.toString()}
          </Col>
        </Row>
        <Row>
          <Col>
            Current Song: {this.state.currentSoundFileName}
          </Col>
          <Col>
            <div style={{width:'40px', float:'left'}}>
              <Play
                onPlay={this.handlePlay}/>
            </div>
            <div style={{width:'40px', float:'left'}}>
              <Next
                onNext={this.handleNext}/>
            </div>
            <div style={{width:'40px', float:'left'}}>
              <Stop
                onStop={this.handleStop}/>
            </div>
          </Col>
          <Col>
            Next Song: {this.state.nextSoundFileName}
          </Col>
        </Row>
      </div>
    )
  }
}
