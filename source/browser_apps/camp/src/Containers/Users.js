import "./Users.css";
import React, {Component} from "react";
import cAmpService from "../Services/cAmpService";
import {Row, Col, Table} from "react-bootstrap";
import { BsFillPersonPlusFill } from "react-icons/bs";
import { Link } from 'react-router-dom'
import {toast} from "react-toastify";

export default class Users extends Component {
  constructor(props) {
    super(props);

    this.state = {
      users: []
    };

    this.loadUsers = this.loadUsers.bind(this);
    this.renderTableBody = this.renderTableBody.bind(this);
  }

  componentDidMount() {
    this.loadUsers();
  }

  loadUsers() {
    cAmpService.getUsers()
      .then(r => {
        this.setState({
          users: r
        });
      })
      .catch(e => {
        toast.error(e.message);
      });
  }

  renderTableHeader() {
      return (
        <thead>
        <tr>
          <th>Email</th>
          <th>Name</th>
        </tr>
        </thead>);
  }

  renderTableBody(list) {
    if (list != null
      && list.length > 0) {
      let rows =  list.map((item, i) => {
        let url = `/users/${item.id}`;
        let firstName = item.firstName ?? '';
        let lastName = item.lastName ?? '';

        let name = `${firstName} ${lastName}`.trim();
        let username = item.username;

        return (
          <tr key={item.id}>
            <td>
              <Link to={url}>
                {username}
              </Link>
            </td>
            <td>{name}</td>
          </tr>);
      });

      return (
        <tbody>
        {rows}
        </tbody>);
    }

    return(
      <tbody/>);
  }

  render() {
    return (
      <div className="Users">
        <Row>
          <Col>
            <h3>Users</h3>
          </Col>
          <Col>
            <div className="float-md-right">
              <Link to={`/users/new`}>
                <BsFillPersonPlusFill/>
              </Link>
            </div>
          </Col>
        </Row>
        <Row>
          <Col>
            <Table striped bordered hover>
              { this.renderTableHeader() }
              { this.renderTableBody(this.state.users) }
            </Table>
          </Col>
        </Row>
      </div>);
  }
}