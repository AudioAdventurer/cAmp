import React, {Component} from 'react';
import {Row, Col} from "react-bootstrap";
import { BsFillSkipStartFill } from "react-icons/bs";

export default class Previous extends Component {
  render() {
    return (<Row>
      <Col>
        <div className="float-md-right">
          <span id="refresh"
                onClick={() => this.props.onPrevious(this.props.soundFileId)}>
            <BsFillSkipStartFill/>
          </span>
        </div>
      </Col>
    </Row>);
  }
}