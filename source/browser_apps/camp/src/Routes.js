import React from "react";
import {Route, Switch} from "react-router-dom";
import AppliedRoute from "./Components/AppliedRoute";
import AuthenticatedRoute from "./Components/AuthenticatedRoute";
import UnauthenticatedRoute from "./Components/UnauthenticatedRoute";

export default ({childProps}) =>
  <Switch>
    <AppliedRoute path="/" exact component={Home} props={childProps}/>
    <Route component={NotFound}/>
  </Switch>;