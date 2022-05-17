import "./Queue.css";
import React, {Component} from "react";
import cAmpService from "../Services/cAmpService";
import {Row, Col, Table} from "react-bootstrap";
import { Link } from 'react-router-dom'
import {toast} from "react-toastify";
import Refresh from "../Components/Refresh";
import Clear from "../Components/Clear";
import Shuffle from "../Components/Shuffle";

export default class Queue extends Component {
  constructor(props) {
    super(props);

    this.state = {
      soundFiles:[]
    };

    this.loadSoundFiles = this.loadSoundFiles.bind(this);
    this.shuffle = this.shuffle.bind(this);

    this.handleRefresh = this.handleRefresh.bind(this);
    this.handleClear = this.handleClear.bind(this);
    this.handleShuffle = this.handleShuffle.bind(this);
  }

  componentDidMount() {
    this.loadSoundFiles();
  }

  componentDidUpdate(prevProps, prevState, snapshot) {
    if (this.props.currentQueuePlayerSoundFileId === null
        && prevProps.currentQueuePlayerSoundFileId !== null){
      this.handleRefresh();
    } else if (this.props.currentQueuePlayerSoundFileId !== prevProps.currentQueuePlayerSoundFileId) {
      this.handleRefresh();
    }
  }

  loadSoundFiles() {
    cAmpService.listQueue()
      .then(r => {
        this.setState({
          soundFiles: r
        });
      })
      .catch(e => {
        toast.error(e.message);
      });
  }

  shuffle() {
    cAmpService.shuffleQueue()
      .then(r=> {
        this.loadSoundFiles();
      }).catch(e => {
      toast.error(e.message);
    });
  }

  handleRefresh() {
    this.loadSoundFiles();
  }

  handleShuffle() {
    this.shuffle();
  }

  handleClear() {
    cAmpService.clearQueue()
      .then(r => {
        this.loadSoundFiles();
        this.props.queuePlayerShouldRefresh();
      })
      .catch(e => {
        toast.error(e.message);
      });

  }

  renderTableBody(list) {
    if (list != null
      && list.length > 0) {
      let rows =  list.map((item, i) => {

        if (item.id != null) {
          let artistUrl = `/artists/${item.artistId}`;
          let albumsUrl = `/albums/${item.albumId}`;

          let sequence = i;
          if (sequence === 0) {
            sequence = 'Current';
          }

          return (
            <tr key={item.id}>
              <td>{sequence}</td>
              <td>{item.title}</td>
              <td><Link to={artistUrl}>{item.artist}</Link></td>
              <td><Link to={albumsUrl}>{item.album}</Link></td>
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

    return(
      <tbody/>);
  }

  render() {
    return (
      <div className="Queue">
        <Row>
          <Col>
            <h3 style={{width:'80px', float:'left'}}>
              Queue
            </h3>
          </Col>
          <Col>
            <div style={{width:'120px', float:'right'}}>
              <div style={{width:'40px', float:'left'}}>
                <Shuffle
                  onShuffle={this.handleShuffle}
                />
              </div>
              <div style={{width:'40px', float:'left'}}>
                <Refresh
                  onRefresh={this.handleRefresh}
                />
              </div>
              <div style={{width:'40px', float:'right'}}>
                <Clear
                  onClear={this.handleClear}
                />
              </div>
            </div>
          </Col>
        </Row>
        <Row>
          <Col>
            <Table striped bordered hover>
              <thead>
              <tr>
                <th>Sequence</th>
                <th>Title</th>
                <th>Artist</th>
                <th>Album</th>
              </tr>
              </thead>
              { this.renderTableBody(this.state.soundFiles) }
            </Table>
          </Col>
        </Row>
      </div>
    );
  }
}