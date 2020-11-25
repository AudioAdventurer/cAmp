import "./User.css";
import React, {Component} from "react";
import {Row, Col, Form, Button} from "react-bootstrap";
import { Link, Redirect } from 'react-router-dom'
import { v4 as uuidv4} from 'uuid';
import {toast} from "react-toastify";
import cAmpService from "../Services/cAmpService";

export default class User extends Component {
  constructor(props) {
    super(props);

    let userId = this.props.match.params.userId;

    this.state = {
      redirect: false,
      userId: userId,
      username: "",
      firstName: "",
      lastName: "",
    };

    this.loadUser = this.loadUser.bind(this);
  }

  componentDidMount() {
    if (this.state.userId !== 'new') {
      this.loadUser(this.state.userId);
    }
  }

  loadUser(userId) {
    cAmpService.getUser(userId)
      .then(r => {
        this.setState({
          userId: r.id,
          firstName: r.firstName ?? '',
          lastName: r.lastName ?? '',
          username: r.username ?? ''
        });
      })
      .catch(e => {
        toast.error(e.message);
      });
  }

  handleChange = event => {
    this.setState({
      [event.target.id]: event.target.value
    });
  };

  renderRedirect = () => {
    if (this.state.redirect) {
      return <Redirect to={`/users`} />
    }
  };

  handleSubmit = event => {
    event.preventDefault();

    try {
      let userId = this.state.userId;
      if (userId==='new'){
        userId = uuidv4();
      }

      let firstName = this.state.firstName;

      let lastName = this.state.lastName;
      if (lastName === "") {
        lastName = null;
      }

      let username = this.state.username;
      if (username === "") {
        username = null;
      }

      let user = {
        Id: userId,
        FirstName: firstName,
        LastName: lastName,
        Username: username
      };

      cAmpService.saveUser(user)
        .then(r => {
          this.setState({
              redirect: true
          });
        })
        .catch(e => {
          toast.error(e.message);
        })
    } catch (e) {
      toast.error(e.message);
    }
  };

  render() {
    return (
      <div className="User">
        {this.renderRedirect()}
        <Row>
          <Col>
            <h3>User: {`${this.state.firstName} ${this.state.lastName}`.trim()}</h3>
          </Col>
          <Col>
            <div className="float-md-right">
              <Link to={`/users`}>Return to Users</Link>
            </div>
          </Col>
        </Row>
        <Row>
          <Col>
            <Form onSubmit={this.handleSubmit}>
              <Form.Group controlId="firstName">
                <Form.Label>First Name</Form.Label>
                <Form.Control
                  type="text"
                  placeholder="Enter user first name"
                  value={this.state.firstName}
                  onChange={this.handleChange}
                />
              </Form.Group>
              <Form.Group controlId="lastName">
                <Form.Label>Last Name</Form.Label>
                <Form.Control
                  type="text"
                  placeholder="Enter user last name"
                  value={this.state.lastName}
                  onChange={this.handleChange}
                />
              </Form.Group>
              <Form.Group controlId="username">
                <Form.Label>Username</Form.Label>
                <Form.Control
                  type="text"
                  placeholder="Enter user's username"
                  value={this.state.username}
                  onChange={this.handleChange}
                />
              </Form.Group>
              <Button variant="primary" type="submit">
                Save
              </Button>
            </Form>
          </Col>
        </Row>
      </div>
    );
  }
}