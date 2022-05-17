import Environment from "../env.js";
import AlbumDao from "../Data/AlbumDao";
import ArtistDao from "../Data/ArtistDao";
import SoundFileDao from "../Data/SoundFileDao";
import AuthDao from "../Data/AuthDao";
import * as axios from "axios";
import UserDao from "../Data/UserDao";
import QueueDao from "../Data/QueueDao";
import PlayListDao from "../Data/PlayListDao";

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

  //Users
  static getUsers() {
    const dao = new UserDao(Environment.BASE_URL);
    return dao.getUsers();
  }

  static getUser(userId) {
    const dao = new UserDao(Environment.BASE_URL);
    return dao.getUser(userId);
  }

  static saveUser(user) {
    const dao = new UserDao(Environment.BASE_URL);
    return dao.saveUser(user);
  }

  //Queue
  static clearQueue() {
    const dao = new QueueDao(Environment.BASE_URL);
    return dao.clearQueue();
  }

  static listQueue() {
    const dao = new QueueDao(Environment.BASE_URL);
    return dao.listQueue();
  }

  static shuffleQueue() {
    const dao = new QueueDao(Environment.BASE_URL);
    return dao.shuffleQueue();
  }

  static getNextQueueSong() {
    const dao = new QueueDao(Environment.BASE_URL);
    return dao.getNextSong();
  }

  static getCurrentQueueSong() {
    const dao = new QueueDao(Environment.BASE_URL);
    return dao.getCurrentSong();
  }

  static advanceToNextQueueSong() {
    const dao = new QueueDao(Environment.BASE_URL);
    return dao.advanceToNextSong();
  }

  static addSoundFileToQueue(soundFileId){
    const dao = new QueueDao(Environment.BASE_URL);
    return dao.addSoundFileToQueue(soundFileId);
  }

  static addAlbumToQueue(albumId) {
    const dao = new QueueDao(Environment.BASE_URL);
    return dao.addAlbumToQueue(albumId);
  }

  static addArtistToQueue(artistId) {
    const dao = new QueueDao(Environment.BASE_URL);
    return dao.addArtistToQueue(artistId);
  }

  static getQueueSize() {
    const dao = new QueueDao(Environment.BASE_URL);
    return dao.getQueueSize();
  }

  //PlayLists
  static getPlayLists() {
    const dao = new PlayListDao(Environment.BASE_URL);
    return dao.getPlayLists();
  }

  static savePlayList(playList) {
    const dao = new PlayListDao(Environment.BASE_URL);
    return dao.savePlayList(playList);
  }

  static deletePlayList(playListId) {
    const dao = new PlayListDao(Environment.BASE_URL);
    return dao.deletePlayList(playListId);
  }

  static addSoundFileToPlayList(playListId, soundFileId) {
    const dao = new PlayListDao(Environment.BASE_URL);
    return dao.addSoundFileToPlayList(playListId, soundFileId);
  }

  static removeSoundFileFromPlayList(playListId, soundFileId) {
    const dao = new PlayListDao(Environment.BASE_URL);
    return dao.removeSoundFileFromPlayList(playListId, soundFileId);
  }

  static getPlayListSoundFiles(playListId) {
    const dao = new PlayListDao(Environment.BASE_URL);
    return dao.getPlayListSoundFiles(playListId);
  }


  //Favorites
  static addSoundFileToFavorites(soundFileId) {
    const dao = new PlayListDao(Environment.BASE_URL);
    return dao.addSoundFileToFavorites(soundFileId);
  }

  static removeSoundFileFromFavorites(soundFileId) {
    const dao = new PlayListDao(Environment.BASE_URL);
    return dao.removeSoundFileFromFavorites(soundFileId);
  }

  static toggleSoundFileFavorite(soundFileId) {
    const dao = new PlayListDao(Environment.BASE_URL);
    return dao.toggleSoundFileFavorite(soundFileId);
  }

  static getFavoritesSoundFiles() {
    const dao = new PlayListDao(Environment.BASE_URL);
    return dao.getFavoritesSoundFiles();
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