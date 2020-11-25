import React, {Component} from 'react';
import { BsFillSkipEndFill } from "react-icons/bs";

export default class Next extends Component {
  render() {
    return (
      <div className="float-md-right">
        <span id={`next${this.props.id}`}
              onClick={
                () => {
                  if (this.props.soundFileId !== undefined
                      && this.props.soundFileId !== null) {
                    this.props.onNext(this.props.soundFileId)
                  } else {
                    this.props.onNext();
                  }
                }}>
          <BsFillSkipEndFill/>
        </span>
      </div>);
  }
}