import "./PlayLists.css";
import React, {Component} from "react";
import cAmpService from "../services/cAmpService";
import {Row, Col, Table} from "react-bootstrap";
import { Link } from 'react-router-dom'
import {toast} from "react-toastify";

export default class PlayLists extends Component {
  constructor(props) {
    super(props);

    this.state = {
      playLists:[]
    };

    this.loadPlayLists = this.loadPlayLists.bind(this);
    this.handleQueueModified = this.handleQueueModified.bind(this);
  }

  componentDidMount() {
    this.loadPlayLists();
  }

  handleQueueModified() {
    this.props.queuePlayerShouldRefresh();
  }

  loadPlayLists() {
    cAmpService.getPlayLists()
      .then(r => {
        this.setState({
          playLists: r
        });
      })
      .catch(e => {
        toast.error(e.message);
      });
  }


  renderTableBody(list) {
    if (list !== null
      && list.length > 0) {
      let rows =  list.map((item, i) => {

        if (item.id != null) {
          let songsUrl = `/playlists/${item.id}/songs`;
          let editUrl = `/playlists/${item.id}`;

          if (item.name === "Favorites"){
            return (
              <tr key={item.id}>
                <td><Link to={songsUrl}>{item.name}</Link></td>
                <td>&nbsp;</td>
                <td>{item.description}</td>
              </tr>
            );
          } else {
            return (
              <tr key={item.id}>
                <td><Link to={songsUrl}>{item.name}</Link></td>
                <td><Link to={editUrl}>Edit</Link></td>
                <td>{item.description}</td>
              </tr>
            );
          }


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
      <div className="PlayLists">
        <Row>
          <Col>
            <h3>Play Lists</h3>
          </Col>
          <Col>
          </Col>
        </Row>
        <Row>
          <Col>
            <Table striped bordered hover>
              <thead>
              <tr>
                <th>Name</th>
                <th>Action</th>
                <th>Description</th>
              </tr>
              </thead>
              { this.renderTableBody(this.state.playLists) }
            </Table>
          </Col>
        </Row>
      </div>
    );
  }
}