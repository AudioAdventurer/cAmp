import BaseDao from "./BaseDao.js"

export default class UserDao extends BaseDao {

  getUser(userId) {
    return this.read(`/users/${userId}`);
  }

  getUsers() {
    return this.read(`/users`);
  }

  saveUser(user) {
    return this.write(`/users`, user);
  }

  saveVolume(volume) {
    return this.write(`/user/volume/${volume}`);
  }
}