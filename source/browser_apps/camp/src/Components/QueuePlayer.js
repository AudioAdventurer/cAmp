import "./QueuePlayer.css";
import React, { Component } from "react";
import {Row, Col} from "react-bootstrap";
import cAmpService from "../Services/cAmpService";
import {toast} from "react-toastify";
import Play from "./Play";
import Next from "./Next";
import Stop from "./Stop";
import {Howl} from "howler";
import RangeSlider from 'react-bootstrap-range-slider';

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
      howl: null,
      volume: 100
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
    this.handleVolumeChanged = this.handleVolumeChanged.bind(this);
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

  handleVolumeChanged(value) {
    this.setState({
      volume: value
    }, ()=>{
      if (this.state.howl !== null) {
        this.state.howl.volume(value / 100.0)
      }
    });
  }

  refresh() {
    cAmpService.getQueueSize()
      .then(r => {
        this.setState({
          queueSize: r
        }, () => {
          this.props.refreshed();

          if (r > 0) {
            cAmpService.getCurrentQueueSong()
              .then(c => {
                this.setState({
                  currentSoundFile: c,
                  currentSoundFileName: c.title
                })
              })
              .catch(e => {
                toast.error(e.message);
              });

            cAmpService.getNextQueueSong()
              .then(n => {
                this.setState({
                  nextSoundFile: n,
                  nextSoundFileName: n.title
                })
              })
              .catch(e => {
                toast.error(e.message);
              });
          } else {
            this.setState({
              currentSoundFile: null,
              currentSoundFileName: null,
              nextSoundFile: null,
              nextSoundFileName:null
            });
          }
        });
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
        if (r !== null
            && r !== "") {
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
                    nextSoundFile: r,
                    nextSoundFileName: r.title
                  })
                }
              })
              .catch(e => {
                toast.error(e.message);
              });

            this.props.playAdvanced(this.state.currentSoundFile.id);
          });
        } else {
          this.setState({
            currentSoundFile: null,
            currentSoundFileName: null,
            nextSoundFile: null,
            nextSoundFileName: null,
            queueSize: 0
          }, ()=> {
            this.handleStop();
            this.props.playAdvanced(null);
          });
        }
      });
  }

  handleStop() {
      if (this.state.howl != null
          && this.state.howl.playing) {
        this.state.howl.stop();

        if (this.state.currentSoundFile !== null) {
          cAmpService.setSoundFileComplete(this.state.currentSoundFile.id, false)
            .then(r => {

            })
            .catch(e => {
              //Do nothing.  Not critical
            });
        }
      }

    this.setState({
      howl: null,
      blob: null,
      playing: false
    }, ()=> {
      this.props.playStopped();
    });
  }

  handlePlay() {
    if (!this.state.playing
        && this.state.currentSoundFile !== undefined
        && this.state.currentSoundFile !== null) {
      this.setState({
        playing: true
      }, ()=> {
        this.props.playStarted(this.state.currentSoundFile.id);

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
      volume: this.state.volume / 100
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

  getShortenedName(name) {
    if (name === undefined
        || name === null) {
      return "";
    }

    let tempName = name;

    if (name.length > 25) {
      tempName = name.substring(0, 20) + "...";
    }

    return tempName;
  }

  render() {
    return (
      <div className="QueuePlayer">
        <Row >
          <Col>
            Queue Player
          </Col>
          <Col>
            Queue Count: {this.state.queueSize}
          </Col>
          <Col>
            {this.state.playing ? "Playing" : "Stopped" }
          </Col>
          <Col>
            &nbsp;
          </Col>
        </Row>
        <Row style={{height:'40px'}}>
          <Col lg={4}>
            Current: {this.getShortenedName(this.state.currentSoundFileName)}
          </Col>
          <Col lg={2}>
            <div style={{width:'120px'}}>
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
            </div>
          </Col>
          <Col lg={2}>
            <RangeSlider
              value={this.state.volume}
              style={{width:'80px'}}
              size='sm'
              onChange={changeEvent => this.handleVolumeChanged(changeEvent.target.value)}
              />
          </Col>
          <Col lg={4}>
            Next: {this.getShortenedName(this.state.nextSoundFileName)}
          </Col>
        </Row>
      </div>
    )
  }
}
