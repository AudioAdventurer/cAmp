import React, {Component} from 'react';
import { BsFillSkipEndFill } from "react-icons/bs";

export default class Next extends Component {
  render() {
    return (
      <div>
        <span id={`next${this.props.id}`}
              onClick={
                () => {
                  if (this.props.enabled) {
                    if (this.props.soundFileId !== undefined
                        && this.props.soundFileId !== null) {
                        this.props.onNext(this.props.soundFileId)
                    } else {
                        this.props.onNext();
                    }
                  }
                }}>
          <BsFillSkipEndFill/>
        </span>
      </div>);
  }
}