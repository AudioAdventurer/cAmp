import BaseDao from "./BaseDao.js"

export default class AlbumDao extends BaseDao {

  getAlbum(albumId) {
    return this.read(`/albums/${albumId}`);
  }

  getAlbums() {
    return this.read(`/albums`);
  }

}