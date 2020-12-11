import React, {Component} from 'react';
import { BsFillStopFill } from "react-icons/bs";

export default class Stop extends Component {
  render() {
    return (
      <div>
        <span id={`stop${this.props.id}`}
              onClick={() => this.props.onStop()}>
          <BsFillStopFill/>
        </span>
      </div>);
  }
}
