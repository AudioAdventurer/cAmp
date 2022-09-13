import React, {Component} from 'react';
import { BsStar, BsStarFill } from "react-icons/bs";

export default class Favorite extends Component {

  constructor(props) {
    super(props);

    let isFavorite = this.props.isFavorite;
    if (isFavorite === undefined
      || isFavorite === null){
      isFavorite = false;
    }

    this.state = {
      isFavorite: isFavorite
    }

    this.handleClick = this.handleClick.bind(this);
  }

  componentDidUpdate(prevProps, prevState, snapshot) {
    if (this.props.isFavorite !== prevProps.isFavorite) {
      this.setState({
        isFavorite: this.props.isFavorite
      });
    }
  }

  handleClick(){
    this.props.onClick(this.props.soundFileId);

    this.setState({
      isFavorite: !this.state.isFavorite
    });
  }

  render() {
    if (this.state.isFavorite) {
      return (
        <div>
          <span id={`favorite${this.props.soundFileId}`}
                onClick={this.handleClick}>
            <BsStarFill/>
          </span>
        </div>
      );
    } else {
      return (
        <div >
          <span id={`favorite${this.props.soundFileId}`}
                onClick={this.handleClick}>
            <BsStar/>
          </span>
        </div>
      );
    }


  }
}
