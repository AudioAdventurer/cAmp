import React, {Component} from 'react';
import {Row, Col} from "react-bootstrap";
import { BsFillStopFill } from "react-icons/bs";

export default class Stop extends Component {
  render() {
    return (<Row>
      <Col>
        <div className="float-md-right">
          <span id="refresh"
                onClick={() => this.props.onStop()}>
            <BsFillStopFill/>
          </span>
        </div>
      </Col>
    </Row>);
  }
}
