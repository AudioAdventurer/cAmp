import BaseDao from "./BaseDao.js"

export default class PlayListDao extends BaseDao {

  getPlayLists() {
    return this.read(`/playlists`);
  }

  getPlayList(playListId) {
    return this.read(`/playlists/${playListId}`)
  }

  savePlayList(playList) {
    return this.write(`/playlists`, playList);
  }

  deletePlayList(playListId) {
    return this.delete(`/playlists/${playListId}`)
  }

  getPlayListSoundFiles(playListId) {
    return this.read(`/playlists/${playListId}/soundfiles`);
  }

  addSoundFileToPlayList(playListId, soundFileId) {
    return this.write(`/playlists/${playListId}/soundfile/${soundFileId}/add`, null);
  }

  removeSoundFileFromPlayList(playListId, soundFileId) {
    return this.write(`/playlists/${playListId}/soundfile/${soundFileId}/remove`, null);
  }

  getFavoritesSoundFiles() {
    return this.read(`/favorites/soundfiles`);
  }

  addSoundFileToFavorites(soundFileId) {
    return this.write(`/favorites/soundfile/${soundFileId}/add`, null);
  }

  removeSoundFileFromFavorites(soundFileId) {
    return this.write(`/favorites/soundfile/${soundFileId}/remove`, null);
  }

  toggleSoundFileFavorite(soundFileId) {
    return this.write(`/favorites/soundfile/${soundFileId}/toggle`, null);
  }
}