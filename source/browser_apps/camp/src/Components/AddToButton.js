import "./AddToButton.css";
import React, { Component } from "react";
import {Dropdown, DropdownButton} from "react-bootstrap";
import cAmpService from "../Services/cAmpService";
import {toast} from "react-toastify";

export default class LoginButton extends Component {

  constructor(props) {
    super(props);

    this.handleClick = this.handleClick.bind(this);
    this.handleQueueModified = this.handleQueueModified.bind(this);
  }

  handleQueueModified() {
    if (this.props.onQueueModified !== undefined
        && this.props.onQueueModified !== null) {
      this.props.onQueueModified();
    }
  }

  handleClick(item) {
    if (item === 'Queue')
    {
      if (this.props.type === 'album') {
        cAmpService.addAlbumToQueue(this.props.id)
          .then(r => {
            this.handleQueueModified();
          })
          .catch(e => {
            toast.error(e.message);
          });
      } else if (this.props.type === 'artist') {
        cAmpService.addArtistToQueue(this.props.id)
          .then(r => {
            this.handleQueueModified();
          })
          .catch(e => {
            toast.error(e.message);
          });
      } else if (this.props.type === 'soundfile') {
        cAmpService.addSoundFileToQueue(this.props.id)
          .then(r => {
            this.handleQueueModified();
          })
          .catch(e => {
            toast.error(e.message);
          });
      }
    }
  }

  render() {
    return (
      <DropdownButton
        id={this.props.id}
        title="Add to"
        variant="secondary"
        size="sm"
        onSelect={this.handleClick}>
        <Dropdown.Item
          eventKey="Queue"
          >Queue
        </Dropdown.Item>
      </DropdownButton>
    )
  }
}
