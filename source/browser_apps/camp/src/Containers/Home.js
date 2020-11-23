import "./Home.css";
import React, {Component} from "react";
import {Row, Col} from "react-bootstrap";
import {Redirect} from 'react-router-dom'

export default class Home extends Component {
  constructor(props) {
    super(props);

    this.state = {
      isLoading: true,
      redirectToSetup: false
    };
  }

  componentDidMount() {

  }

  renderLander() {
    return (
      <div className="lander">
        <h1>cAmp Audio Player</h1>
        <p>A simple container based audio player designed for home use.</p>
      </div>
    );
  }

  renderProjects() {
    return (
      <div className="HomePanel">
        <Row>
          <Col>
          </Col>
        </Row>
        <Row>
          <Col>
          </Col>
        </Row>
        <Row>
          <Col>
          </Col>
        </Row>
      </div>
    );
  }

  render() {
    if (this.state.redirectToSetup) {
      return (<Redirect to='/setup'/>)
    } else {
      return (
        <div className="Home">
          {this.props.isAuthenticated ? this.renderProjects() : this.renderLander()}
        </div>);
    }
  }
}