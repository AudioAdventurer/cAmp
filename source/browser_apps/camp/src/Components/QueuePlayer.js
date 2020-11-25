import "./QueuePlayer.css";
import React, { Component } from "react";
import {Row, Col} from "react-bootstrap";
import cAmpService from "../Services/cAmpService";
import {toast} from "react-toastify";

export default class QueuePlayer extends Component {

  constructor(props) {
    super(props);

    this.state = {
      username: "",
      playing: false,
      shouldStop: false,
      queueSize: 0
    };

    this.handleShouldStop = this.handleShouldStop.bind(this);
    this.handleShouldRefresh = this.handleShouldRefresh.bind(this);
    this.refresh = this.refresh.bind(this);
  }

  componentDidMount() {
    this.refresh();
  }

  componentDidUpdate(prevProps, prevState, snapshot) {
    if (prevProps.shouldStop !== this.props.shouldStop) {
      this.setState({
        shouldStop: true
      }, ()=> {
        this.handleShouldStop();
      });
    }

    if (prevProps.shouldRefresh !== this.props.shouldRefresh) {
      this.handleShouldRefresh();
    }
  }

  handleShouldStop(){
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
  }

  handleShouldRefresh() {
    this.refresh();
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
      </div>
    )
  }
}
