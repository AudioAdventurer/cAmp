import BaseDao from "./BaseDao.js"

export default class SoundFileDao extends BaseDao {

  getSoundFilesByAlbum(albumId) {
    return this.read(`/albums/${albumId}/soundfiles`);
  }

  getSoundFilesByArtist(artistId) {
    return this.read(`/artists/${artistId}/soundfiles`);
  }

  getSoundFiles() {
    return this.read(`/soundfiles`);
  }

  getSoundFile(soundFileId) {
    return this.readFile(`/soundfiles/${soundFileId}`);
  }

  setSoundFileComplete(soundFileId, playedToEnd) {
    if (playedToEnd) {
      return this.write(`/soundfiles/${soundFileId}/completed`);
    } else {
      return this.write(`/soundfiles/${soundFileId}/skipped`);
    }
  }
}

