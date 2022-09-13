import "./LoginButton.css";
import React, { Component } from "react";
import {Button} from "react-bootstrap";

export default class LoginButton extends Component {

  constructor(props) {
    super(props);

    this.state = {
      username: props.username
    };

    this.handleClick = this.handleClick.bind(this);
  }

  handleClick() {
    this.props.onClick(this.state.username);
  }

  render() {
    return (
      <Button
        className={ `LoginButton ${ this.props.classname }` }
        disabled={ this.props.disabled }
        type={ this.props.type }
        variant={ this.props.variant }
        size={ this.props.size }
        onClick={this.handleClick}
      >
        { this.props.text }
      </Button>
    )
  }
}
