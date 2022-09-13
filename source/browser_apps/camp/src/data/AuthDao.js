import BaseDao from "./BaseDao.js"

export default class AuthDao extends BaseDao {

  getUsersForLogin() {
    return this.read(`/login/users`);
  }

  login(username) {
    return this.write(`/login/${username}`);
  }
}