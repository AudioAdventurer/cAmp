import React from "react";
import {BrowserRouter as Router, Route, Redirect, Switch} from "react-router-dom";
import AppliedRoute from "./Components/AppliedRoute";
import AuthenticatedRoute from "./Components/AuthenticatedRoute";
import UnauthenticatedRoute from "./Components/UnauthenticatedRoute";
import NotFound from "./Containers/NotFound";
import Home from "./Containers/Home";


export default ({childProps}) =>
  <Switch>
    <AppliedRoute path="/" exact component={Home} props={childProps}/>
    <Route component={NotFound}/>
  </Switch>;