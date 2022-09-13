import React, {Component} from 'react';
import { BsShuffle } from "react-icons/bs";

export default class Shuffle extends Component {
  render() {
    return (
      <div>
        <span id={`next${this.props.id}`}
              onClick={
                () => {
                  this.props.onShuffle();
                }}>
          <BsShuffle/>
        </span>
      </div>);
  }
}