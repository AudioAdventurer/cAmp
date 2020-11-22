import React, {Component} from 'react';
import {Row, Col} from "react-bootstrap";
import { BsFillSkipEndFill } from "react-icons/bs";

export default class Play extends Component {
  render() {
    return (<Row>
      <Col>
        <div className="float-md-right">
          <span id="refresh"
                onClick={() => this.props.onNext(this.props.soundFileId)}>
            <BsFillSkipEndFill/>
          </span>
        </div>
      </Col>
    </Row>);
  }
}