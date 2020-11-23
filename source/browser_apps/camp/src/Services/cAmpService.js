import Environment from "../env.js";
import AlbumDao from "../Data/AlbumDao";
import ArtistDao from "../Data/ArtistDao";
import SoundFileDao from "../Data/SoundFileDao";
import AuthDao from "../Data/AuthDao";
import * as axios from "axios";

export default class cAmpService {
  static JWT = "";
  static UserProperties = null;
  static UserAccess = {};

  //Artists
  static getArtists() {
    const dao = new ArtistDao(Environment.BASE_URL);
    return dao.getArtists();
  }

  static getArtist(artistId) {
    const dao = new ArtistDao(Environment.BASE_URL);
    return dao.getArtist(artistId);
  }

  static getAlbumsByArtist(artistId) {
    const dao = new ArtistDao(Environment.BASE_URL);
    return dao.getArtistAlbums(artistId);
  }

  //Albums
  static getAlbums() {
    const dao = new AlbumDao(Environment.BASE_URL);
    return dao.getAlbums();
  }

  static getAlbum(albumId) {
    const dao = new AlbumDao(Environment.BASE_URL);
    return dao.getAlbum(albumId);
  }

  //Sound Files
  static getSoundFiles() {
    const dao = new SoundFileDao(Environment.BASE_URL);
    return dao.getSoundFiles();
  }

  static getSoundFilesByArtist(artistId) {
    const dao = new SoundFileDao(Environment.BASE_URL);
    return dao.getSoundFilesByArtist(artistId);
  }

  static getSoundFilesByAlbum(albumId) {
    const dao = new SoundFileDao(Environment.BASE_URL);
    return dao.getSoundFilesByAlbum(albumId);
  }

  static getSoundFile(soundFileId) {
    const dao = new SoundFileDao(Environment.BASE_URL);
    return dao.getSoundFile(soundFileId);
  }

  static setSoundFileComplete(soundFileId, playedToEnd) {
    const dao = new SoundFileDao(Environment.BASE_URL);
    return dao.setSoundFileComplete(soundFileId, playedToEnd);
  }

  //Authentication
  static login(username) {
    const dao = new AuthDao(Environment.BASE_URL);
    return dao.login(username);
  }

  static getUsersForLogin(username) {
    const dao = new AuthDao(Environment.BASE_URL);
    return dao.getUsersForLogin();
  }

  static logout() {
    this.JWT = "";
  }

  static setJwt(jwt) {
    this.JWT = jwt;
    axios.defaults.headers.common['Authorization'] = 'Bearer ' + this.JWT;
  }

}