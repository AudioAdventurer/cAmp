import React from "react";
import {Route, Switch} from "react-router-dom";
import AppliedRoute from "./Components/AppliedRoute";
import AuthenticatedRoute from "./Components/AuthenticatedRoute";
import UnauthenticatedRoute from "./Components/UnauthenticatedRoute";
import NotFound from "./Containers/NotFound";
import Home from "./Containers/Home";
import Artists from "./Containers/Artists";
import Albums from "./Containers/Albums";
import SoundFiles from "./Containers/SoundFiles";
import PickUser from "./Containers/PickUser";
import Users from "./Containers/Users";
import User from "./Containers/User";


export default ({childProps}) =>
  <Switch>
    <AppliedRoute path="/" exact component={Home} props={childProps}/>
    <AuthenticatedRoute path="/artists" exact component={Artists} props={childProps} />
    <AuthenticatedRoute path="/artists/:artistId" exact component={Artists} props={childProps} />
    <AuthenticatedRoute path="/artists/:artistId/albums" exact component={Albums} props={childProps} />
    <AuthenticatedRoute path="/artists/:artistId/songs" exact component={SoundFiles} props={childProps} />
    <AuthenticatedRoute path="/albums" exact component={Albums} props={childProps} />
    <AuthenticatedRoute path="/albums/:albumId" exact component={Albums} props={childProps} />
    <AuthenticatedRoute path="/albums/:albumId/songs" exact component={SoundFiles} props={childProps} />
    <AuthenticatedRoute path="/songs" exact component={SoundFiles} props={childProps} />
    <AuthenticatedRoute path="/users" exact component={Users} props={childProps} />
    <AuthenticatedRoute path="/users/:userId" exact component={User} props={childProps} />
    <UnauthenticatedRoute path="/login" exact component={PickUser} props={childProps} />
    <Route component={NotFound}/>
  </Switch>;