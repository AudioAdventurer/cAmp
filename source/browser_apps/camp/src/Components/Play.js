import React, {Component} from 'react';
import {Row, Col} from "react-bootstrap";
import { BsFillPlayFill } from "react-icons/bs";

export default class Play extends Component {
  render() {
    return (<Row>
      <Col>
        <div className="float-md-right">
          <span id="refresh"
                onClick={() => this.props.onPlay()}>
            <BsFillPlayFill/>
          </span>
        </div>
      </Col>
    </Row>);
  }
}
