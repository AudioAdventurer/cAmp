docker stop cAmp

REM Remove existing
docker rm cAmp

REM Run the new image
docker run -d  ^
		   -p 8000:8000 ^
		   -e "CAMP_MUSIC_FOLDER=/Media/Music" ^
		   -e "CAMP_DATA_FOLDER=/Media/Data" ^
		   -v c:\Media\Data:/Media/Data ^
		   -v c:\Media\Music:/Media/Music ^
		   --restart unless-stopped ^
		   --name=cAmp ^
		   "audioadventurer/camp"