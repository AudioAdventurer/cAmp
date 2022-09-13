class Environment {
  static setBaseUrl() {
    let getUrl = window.location;

    //For production api port is the same as web port
    Environment.BASE_URL = getUrl.protocol + "//" + getUrl.host + "/api";
  }


  static BASE_URL = "";
}

export default Environment;

Environment.setBaseUrl();