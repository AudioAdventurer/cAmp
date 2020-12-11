import React, {Component} from 'react';
import {Row, Col} from "react-bootstrap";
import { BsFillPlayFill } from "react-icons/bs";

export default class Play extends Component {
  render() {
    return (
      <Row>
        <Col>
          <div>
            <span id="refresh"
                  onClick={() => this.props.onPlay(this.props.soundFileId)}>
              <BsFillPlayFill/>
            </span>
          </div>
        </Col>
      </Row>);
  }
}