import React, {Component} from 'react';
import { BsStar, BsStarFill } from "react-icons/bs";

export default class Favorite extends Component {
  render() {
    let isFavorite = this.props.isFavorite;
    if (isFavorite === undefined
        || isFavorite === null){
      isFavorite = false;
    }

    if (isFavorite) {
      return (
        <div className="float-md-right">
          <span id={`favorite${this.props.soundFileId}`}
                onClick={() => this.props.onClick(this.props.soundFileId)}>
            <BsStarFill/>
          </span>
        </div>
      );
    } else {
      return (
        <div >
          <span id={`favorite${this.props.soundFileId}`}
                onClick={() => this.props.onClick(this.props.soundFileId)}>
            <BsStar/>
          </span>
        </div>
      );
    }


  }
}
