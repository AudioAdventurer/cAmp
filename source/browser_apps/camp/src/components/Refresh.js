import React, {Component} from 'react';
import { GrUpdate } from "react-icons/gr";

export default class Refresh extends Component {
  render() {
    return (
      <div className="float-md-right" style={{width:"30px"}}>
        <span id={`play${this.props.id}`}
              onClick={() => this.props.onRefresh()}>
          <GrUpdate/>
        </span>
      </div>
    );
  }
}
