import React, {Component} from 'react';
import { BsFillTrashFill } from "react-icons/bs";

export default class Clear extends Component {
  render() {
    return (
      <div className="float-md-right" style={{width:"30px"}}>
        <span id={`play${this.props.id}`}
              onClick={() => this.props.onClear()}>
          <BsFillTrashFill/>
        </span>
      </div>
    );
  }
}