class Environment {
  static setBaseUrl() {
    let getUrl = window.location;

    //Allows API and Node to be on different ports for development.  Api-8000, Node: 3000
    Environment.BASE_URL = getUrl.protocol + "//" + getUrl.host.split(":")[0] + ":8000/api";
  }

  static BASE_URL = "";
}

export default Environment;

Environment.setBaseUrl();