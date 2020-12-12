import "./Albums.css";
import React, {Component} from "react";
import cAmpService from "../Services/cAmpService";
import {Row, Col, Table} from "react-bootstrap";
import { Link } from 'react-router-dom'
import {toast} from "react-toastify";
import AddToButton from "../Components/AddToButton";

export default class Albums extends Component {
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
      albumId: albumId,
      artistId: artistId,
      albums:[]
    };

    this.loadAlbums = this.loadAlbums.bind(this);
    this.handleQueueModified = this.handleQueueModified.bind(this);
  }

  componentDidMount() {
    this.loadAlbums();
  }

  handleQueueModified() {
    this.props.queuePlayerShouldRefresh();
  }

  loadAlbums() {
    if (this.state.artistId !== null) {
      cAmpService.getAlbumsByArtist(this.state.artistId)
        .then(r => {
          this.setState({
            albums: r
          });
        })
        .catch(e => {
          toast.error(e.message);
        });
    } else if (this.state.albumId !== null) {
      cAmpService.getAlbum(this.state.albumId)
        .then(r => {
          this.setState({
            albums: [r]
          });
        })
        .catch(e => {
          toast.error(e.message);
        });
    } else {
      cAmpService.getAlbums()
        .then(r => {
          this.setState({
            albums: r
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
          let albumsUrl = `/albums/${item.id}/songs`;

          return (
            <tr key={item.id}>
              <td>{item.name}</td>
              <td><Link to={artistUrl}>{item.artist}</Link></td>
              <td><Link to={albumsUrl}>Songs</Link></td>
              <td>
                <AddToButton
                  id={item.id}
                  type="album"
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

    return(
      <tbody/>);
  }

  render() {
    return (
      <div className="Albums">
        <Row>
          <Col>
            <h3>Albums</h3>
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
                <th>Songs</th>
                <th>Action</th>
              </tr>
              </thead>
              { this.renderTableBody(this.state.albums) }
            </Table>
          </Col>
        </Row>
      </div>
    );
  }
}