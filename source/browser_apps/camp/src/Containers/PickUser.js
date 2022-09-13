import "./Artists.css";
import React, {Component} from "react";
import cAmpService from "../Services/cAmpService";
import {Row, Col, Table} from "react-bootstrap";
import {toast} from "react-toastify";
import LoginButton from "../Components/LoginButton";
import {Navigate} from "react-router-dom";

export default class PickUser extends Component {
  constructor(props) {
    super(props);

    this.state = {
      users:[],
      userSelected: false
    };

    this.login = this.login.bind(this);
    this.loadUsers = this.loadUsers.bind(this);
  }

  componentDidMount() {
    this.loadUsers();
  }

  loadUsers() {
    cAmpService.getUsersForLogin()
      .then(r => {
        this.setState({
          users: r
        });
      })
      .catch(e => {
        toast.error(e.message);
      });
  }

  login(username) {
    cAmpService.login(username)
      .then(r => {
        let jwt = r.jwt;
        let volume = r.volume;

        cAmpService.setJwt(jwt);
        cAmpService.setVolume(volume);

        this.props.userHasAuthenticated(true);

        this.setState({userSelected:true})
      })
      .catch(e => {
        toast.error(e.message);
      });

  }

  renderTableBody(list) {
    if (list != null
      && list.length > 0) {
      let rows =  list.map((item, i) => {

        if (item.id != null) {
          return (
            <tr key={item.id}>
              <td><LoginButton variant="primary"
                     username={item.username}
                     text="Login"
                     onClick={this.login}/>
              </td>
              <td>{item.username}</td>
              <td>{item.firstName}</td>
              <td>{item.lastName}</td>
            </tr>
          );
        } else {
          return "";
        }
      });

      return (
        <tbody>
        {rows}
        </tbody>);
    }

    return(
      <tbody></tbody>);
  }

  render() {
    if (this.state.userSelected) {
      return (<Navigate to='/'/>)
    } else {
      return (
        <div className="PickUser">
          <Row>
            <Col>
              <h3>Pick User</h3>
            </Col>
            <Col>
            </Col>
          </Row>
          <Row>
            <Col>
              <Table striped bordered hover>
                <thead>
                <tr>
                  <th>Action</th>
                  <th>User Name</th>
                  <th>First Name</th>
                  <th>Last Name</th>
                </tr>
                </thead>
                {this.renderTableBody(this.state.users)}
              </Table>
            </Col>
          </Row>
        </div>
      );
    }
  }
}