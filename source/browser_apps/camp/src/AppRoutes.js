import React, {Suspense} from "react";
import {Navigate, Route, Routes, useParams} from "react-router-dom";
import Environment from "./env";

// const Artists = lazy(() => import('./containers/Artists'));
// const Albums = lazy(()=> import('./containers/Albums'));
// const Home = lazy(() => import('./containers/Home'));
// const SoundFiles = lazy(()=> import('./containers/SoundFiles'));
// const PickUser = lazy(()=>import('./containers/PickUser'));
// const PlayLists = lazy(()=> import('./containers/PlayLists'));
// const Queue = lazy(()=> import('./containers/Queue'));
// const Users = lazy(()=> import("./containers/Users"));
// const User = lazy(()=> import('./containers/User'));

import Home from "./containers/Home";
import Artists from "./containers/Artists";
import Albums from "./containers/Albums";
import PickUser from "./containers/PickUser";
import PlayLists from "./containers/PlayLists"
import Queue from "./containers/Queue";
import SoundFiles from "./containers/SoundFiles";
import Users from "./containers/Users";
import User from "./containers/User";

const AppRoutes = ({childProps}) => (
  <Suspense fallback={<div>Loading ...</div>}>
      <Routes>
          <Route path="/"
                 exact
                 element={<Home {...childProps}/>}
          />
          <Route path="/login"
                 exact
                 element={<PickUser {...childProps}/>}
          />
          <Route path="/artists"
                 exact
                 element={
                   <RequireAuthentication
                     child={<WithParams Component={Artists} childProps={childProps}/>}
                     childProps={childProps}
                   />
                 }
          />
          <Route path="/artists/:artistId"
                 exact
                 element={
                     <RequireAuthentication
                       child={<WithParams Component={Artists} childProps={childProps}/>}
                       childProps={childProps}
                     />
                 }
          />
          <Route path="/artists/:artistId/albums"
                 exact
                 element={
                     <RequireAuthentication
                       child={<WithParams Component={Albums} childProps={childProps}/>}
                       childProps={childProps}
                     />
                 }
          />
          <Route path="/artists/:artistId/songs"
                 exact
                 element={
                     <RequireAuthentication
                       child={<WithParams Component={SoundFiles} childProps={childProps}/>}
                       childProps={childProps}
                     />
                 }
          />
          <Route path="/albums"
                 exact
                 element={
                   <RequireAuthentication
                     child={<WithParams Component={Albums} childProps={childProps}/>}
                     childProps={childProps}
                   />
                 }
          />
          <Route path="/albums/:albumId"
                 exact
                 element={
                   <RequireAuthentication
                     child={<WithParams Component={Albums} childProps={childProps}/>}
                     childProps={childProps}
                   />
                 }
          />
          <Route path="/albums/:albumId/songs"
                 exact
                 element={
                   <RequireAuthentication
                     child={<WithParams Component={SoundFiles} childProps={childProps}/>}
                     childProps={childProps}
                   />
                 }
          />
          <Route path="/playlists"
                 exact
                 element={<RequireAuthentication child={<PlayLists {...childProps}/>} childProps={childProps}/>}
          />
          <Route path="/playlists/:playListId/songs"
                 exact
                 element={
                     <RequireAuthentication
                       child={<WithParams Component={SoundFiles} childProps={childProps}/>}
                       childProps={childProps}
                     />
                 }
          />
          <Route path="/queue"
                 exact
                 element={<RequireAuthentication child={<Queue {...childProps}/>} childProps={childProps}/>}
          />
          <Route path="/songs"
                 exact
                 element={
                   <RequireAuthentication
                     child={<WithParams Component={SoundFiles} childProps={childProps}/>}
                     childProps={childProps}
                   />
                 }
          />
          <Route path="/users"
                 exact
                 element={<RequireAuthentication child={<Users {...childProps}/>} childProps={childProps}/>}
          />
          <Route path="/users/:userId"
                 exact
                 element={
                     <RequireAuthentication
                       child={<WithParams Component={User} childProps={childProps}/>}
                       childProps={childProps}
                     />
                 }
          />
      </Routes>
  </Suspense>
);

function RequireAuthentication({child, childProps, redirectTo}) {
    if (redirectTo == null){
        redirectTo = `${Environment.BASE_URL}`
    }

    return childProps.userHasAuthenticated ? child : <Navigate to={redirectTo}/>;
}

function WithParams({Component, childProps}) {
    return (<Component {...childProps} params={useParams()}/>);
}

export default AppRoutes;