REM Stop image if running
docker stop cAmp

REM Remove existing
docker rm cAmp

REM Pull the latest image
docker pull audioadventurer/camp:latest

REM Run the new image
REM Adjust your media locations as needed (c:\Media\Data, c:\Media\Music)
REM The data folder is where the database will be created to track playlists and history
docker run -d  ^
		   -p 8080:8000 ^
		   -e "CAMP_MUSIC_FOLDER=/Media/Music" ^
		   -e "CAMP_DATA_FOLDER=/Media/Data" ^
		   -v c:\Media\Data:/Media/Data ^
		   -v c:\Media\Music:/Media/Music ^
		   --restart unless-stopped ^
		   --name=cAmp ^
		   "audioadventurer/camp"