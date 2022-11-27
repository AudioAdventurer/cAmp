import "./App.css";
import React, { Component } from "react";
import { Link } from "react-router-dom";
import Routes from "./AppRoutes";
import { LinkContainer } from "react-router-bootstrap";
import cAmpService from "./services/cAmpService";
import {Container, Nav, Navbar} from "react-bootstrap";
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import QueuePlayer from "./components/QueuePlayer";

export default class App extends Component {
  constructor(props) {
    super(props);

    this.state = {
      isAuthenticated: false,
      isAuthenticating: true,
      messageList:[],
      queuePlayerPlaying: false,
      queuePlayerShouldStop: false,
      queuePlayerShouldRefresh: false,
      currentQueuePlayerSoundFileId: null,
      currentSong: null,
      nextSong: null
    };

    this.handleQueuePlayerPlayStarted = this.handleQueuePlayerPlayStarted.bind(this);
    this.handleQueuePlayerPlayStopped = this.handleQueuePlayerPlayStopped.bind(this);
    this.handleQueuePlayerPlayAdvanced = this.handleQueuePlayerPlayAdvanced.bind(this);
    this.handleQueuePlayerRefreshed = this.handleQueuePlayerRefreshed.bind(this);

    this.handleQueuePlayerShouldStop = this.handleQueuePlayerShouldStop.bind(this);
    this.handleQueuePlayerShouldRefresh = this.handleQueuePlayerShouldRefresh.bind(this);
  }

  userHasAuthenticated = authenticated => {
    this.setState({ isAuthenticated: authenticated });
  };

  handleLogout = event => {
    cAmpService.logout();
    this.userHasAuthenticated(false);
  };

  handleQueuePlayerPlayStarted(currentSoundFileId) {
    this.setState({
      queuePlayerPlaying: true,
      currentQueuePlayerSoundFileId: currentSoundFileId
    });
  }

  handleQueuePlayerPlayStopped() {
    this.setState({
      queuePlayerPlaying: false,
      queuePlayerShouldStop: false
    });
  }

  handleQueuePlayerPlayAdvanced(currentSoundFileId) {
    this.setState({
      currentQueuePlayerSoundFileId: currentSoundFileId
    });
  }

  handleQueuePlayerShouldStop(){
    this.setState({
      queuePlayerShouldStop: true
    });
  }

  handleQueuePlayerRefreshed() {
    this.setState({
      queuePlayerShouldRefresh: false
    });
  }

  handleQueuePlayerShouldRefresh() {
    this.setState({
      queuePlayerShouldRefresh: true
    });
  }

  renderQueuePlayer() {
    return (
      <QueuePlayer
        playStarted={this.handleQueuePlayerPlayStarted}
        playStopped={this.handleQueuePlayerPlayStopped}
        playAdvanced={this.handleQueuePlayerPlayAdvanced}
        refreshed={this.handleQueuePlayerRefreshed}
        shouldStop={this.state.queuePlayerShouldStop}
        shouldRefresh={this.state.queuePlayerShouldRefresh}
      />
    );
  }

  renderLoggedInNavBarStart() {
    return(
      <Navbar.Collapse id="basic-navbar-nav">
        <Nav className="mr-auto">
          <Nav.Item>
            <LinkContainer to="/artists">
              <Nav.Link>
                Artists
              </Nav.Link>
            </LinkContainer>
          </Nav.Item>
          <Nav.Item>
            <LinkContainer to="/albums">
              <Nav.Link>
                Albums
              </Nav.Link>
            </LinkContainer>
          </Nav.Item>
          <Nav.Item>
            <LinkContainer to="/songs">
              <Nav.Link>
                Songs
              </Nav.Link>
            </LinkContainer>
          </Nav.Item>
          <Nav.Item>
            <LinkContainer to="/playlists">
              <Nav.Link>
                Play Lists
              </Nav.Link>
            </LinkContainer>
          </Nav.Item>
          <Nav.Item>
            <LinkContainer to="/queue">
              <Nav.Link>
                Queue
              </Nav.Link>
            </LinkContainer>
          </Nav.Item>
        </Nav>
      </Navbar.Collapse>
    );
  }

  renderLoggedInNavBarEnd() {
    return(
        <Navbar.Collapse id="basic-navbar-nav" className="justify-content-end">
          <Nav >
            <Nav.Item>
              <LinkContainer to="/users">
                <Nav.Link>
                  Users
                </Nav.Link>
              </LinkContainer>
            </Nav.Item>
            <Nav.Item onClick={this.handleLogout} className="justify-content-end" >
              <Nav.Link>
                Logout
              </Nav.Link>
            </Nav.Item>
          </Nav>
        </Navbar.Collapse>
    );
  }

  renderNotLoggedInNavBar() {
    return (
      <Navbar.Collapse className="justify-content-end">
        <Nav className="mr-auto">
        </Nav>
        <Nav >
          <Nav.Item>
            <LinkContainer to="/login">
              <Nav.Link>Login</Nav.Link>
            </LinkContainer>
          </Nav.Item>
        </Nav>
      </Navbar.Collapse>
    );
  }

  renderNavbar() {
    return (
      <Navbar bg="light" expand="lg">
        <Container>
          <Navbar.Brand>
            <Link to="/">cAmp Audio Player</Link>
          </Navbar.Brand>
          {this.state.isAuthenticated ? this.renderLoggedInNavBarStart() : this.renderNotLoggedInNavBar() }
          {this.state.isAuthenticated ? this.renderLoggedInNavBarEnd() : ""}
        </Container>
      </Navbar>
    );
  }

  render() {
    const childProps = {
      isAuthenticated: this.state.isAuthenticated,
      userHasAuthenticated: this.userHasAuthenticated,
      queuePlayerPlaying: this.state.queuePlayerPlaying,
      queuePlayerShouldStop: this.handleQueuePlayerShouldStop,
      queuePlayerShouldRefresh: this.handleQueuePlayerShouldRefresh,
      currentQueuePlayerSoundFileId: this.state.currentQueuePlayerSoundFileId
    };

    return (
      <div className="App container">
        <ToastContainer
          position="top-center"
          autoClose={false}
          closeOnClick
        />
        { this.renderNavbar() }
        { this.state.isAuthenticated ? this.renderQueuePlayer() : "" }
        <Routes childProps={childProps} />
      </div>
    );
  }
}