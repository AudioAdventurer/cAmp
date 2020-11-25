import BaseDao from "./BaseDao.js"

export default class QueueDao extends BaseDao {
  listQueue() {
    return this.read(`/queue`);
  }

  getNextSong() {
    return this.read(`/queue/next`);
  }

  getCurrentSong() {
    return this.read(`/queue/current`);
  }

  advanceToNextSong() {
    return this.read(`/queue/advance`);
  }

  getQueueSize() {
    return this.read(`/queue/size`);
  }

  addSoundFileToQueue(soundFileId) {
    return this.write(`/queue/song/${soundFileId}`);
  }

  addAlbumToQueue(albumId) {
    return this.write(`/queue/album/${albumId}`);
  }

  addArtistToQueue(artistId) {
    return this.write(`/queue/artist/${artistId}`);
  }

  clearQueue() {
    return this.write(`/queue/clear`);
  }
}

