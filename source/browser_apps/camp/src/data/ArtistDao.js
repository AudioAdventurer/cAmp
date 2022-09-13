import BaseDao from "./BaseDao.js"

export default class ArtistDao extends BaseDao {

  getArtist(artistId) {
    return this.read(`/artists/${artistId}`);
  }

  getArtists() {
    return this.read(`/artists`);
  }

  getArtistAlbums(artistId) {
    return this.read(`/artists/${artistId}/albums`);
  }

  getArtistSoundFiles(artistId) {
    return this.read(`/artists/${artistId}/soundfiles`);
  }
}