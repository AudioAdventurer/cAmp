import "./Artists.css";
import React, {Component} from "react";
import cAmpService from "../Services/cAmpService";
import {Row, Col, Table} from "react-bootstrap";
import { Link } from 'react-router-dom'
import {toast} from "react-toastify";

export default class Artists extends Component {
  constructor(props) {
    super(props);

    let artistId = this.props.match.params.artistId;
    if (artistId === undefined) {
      artistId = null;
    }

    this.state = {
      artistId: artistId,
      artists:[]
    };
  }

  componentDidMount() {
    this.loadArtists();
  }

  loadArtists() {
    if (this.state.artistId !== null) {
      cAmpService.getArtist(this.state.artistId)
        .then(r => {
          this.setState({
            artists: [r]
          });
        })
        .catch(e => {
          toast.error(e.message);
        });
    } else {
      cAmpService.getArtists()
        .then(r => {
          this.setState({
            artists: r
          });
        })
        .catch(e => {
          toast.error(e.message);
        });
    }
  }

  renderTableBody(list) {
    if (list != null
      && list.length > 0) {
      let rows =  list.map((item, i) => {

        if (item.id != null) {
          let songsUrl = `/artists/${item.id}/songs`;
          let albumsUrl = `/artists/${item.id}/albums`;

          return (
            <tr key={item.id}>
              <td>{item.name}</td>
              <td><Link to={songsUrl}>Songs</Link></td>
              <td><Link to={albumsUrl}>Albums</Link></td>
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
      <tbody></tbody>);
  }

  render() {
    return (
      <div className="Artists">
        <Row>
          <Col>
            <h3>Artists</h3>
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
                <th>Songs</th>
                <th>Albums</th>
              </tr>
              </thead>
              { this.renderTableBody(this.state.artists) }
            </Table>
          </Col>
        </Row>
      </div>
    );
  }
}