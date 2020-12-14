#!/bin/bash

#Pull the latest image
docker pull audioadventurer/camp

#Did we successfully pull it (So we don't stop and remove when there isn't a reason)
status=$?

if test $status -eq 0
then
	#Stop image if running
	docker stop cAmp

	#Remove existing
	docker rm cAmp

	#Run the new image
	#Adjust your media locations as needed (c:\Media\Data, c:\Media\Music)
	#The data folder is where the database will be created to track playlists and history
	docker run -d  ^
			   -p 8080:8000 ^
			   -e "CAMP_MUSIC_FOLDER=/Media/Music" ^
			   -e "CAMP_DATA_FOLDER=/Media/Data" ^
			   -v c:\Media\Data:/Media/Data ^
			   -v c:\Media\Music:/Media/Music ^
			   --restart unless-stopped ^
			   --name=cAmp ^
			   "audioadventurer/camp"
		   
else
	echo "Unable to pull specified tag"
fi