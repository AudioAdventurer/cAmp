import "./SoundFiles.css";
import React, {Component} from "react";
import cAmpService from "../Services/cAmpService";
import {Row, Col, Table} from "react-bootstrap";
import { Link } from 'react-router-dom'
import {toast} from "react-toastify";

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

    this.state = {
      artistId: artistId,
      albumId: albumId,
      soundFiles:[]
    };
  }

  componentDidMount() {
    this.loadSongs();
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

  renderTableBody(list) {
    if (list != null
      && list.length > 0) {
      let rows =  list.map((item, i) => {

        if (item.id != null) {
          let artistUrl = `/artists/${item.artistId}`;
          let albumsUrl = `/artists/${item.albumId}`;

          return (
            <tr key={item.id}>
              <td>{item.title}</td>
              <td><Link to={artistUrl}>{item.artist}</Link></td>
              <td><Link to={albumsUrl}>{item.album}</Link></td>
              <td>&nbsp;</td>
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
                <th>Name</th>
                <th>Artist</th>
                <th>Album</th>
                <th>Action</th>
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