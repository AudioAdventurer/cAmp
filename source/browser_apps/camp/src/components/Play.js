import React, {Component} from 'react';
import { BsFillPlayFill } from "react-icons/bs";

export default class Play extends Component {
  render() {
    return (
      <div>
        <span id={`play${this.props.id}`}
              onClick={() => this.props.onPlay()}>
          <BsFillPlayFill/>
        </span>
      </div>
    );
  }
}
