mergeInto(LibraryManager.library, {	
	OnApplicationRunScreenOrientationRequest: function (){
		if(window.innerHeight > window.innerWidth){
			gameInstance.SendMessage('ScreenOrientationDetector', 'ReciveOrientationChangedResult', 0);
		}
		else{
			gameInstance.SendMessage('ScreenOrientationDetector', 'ReciveOrientationChangedResult', 1);
		}
	},
	InitializePluginForDevices: function () {
		window.onorientationchange = function (event) {
		if(window.innerHeight > window.innerWidth){
			gameInstance.SendMessage('ScreenOrientationDetector', 'ReciveOrientationChangedResult', 0);
		}
		else{
			gameInstance.SendMessage('ScreenOrientationDetector', 'ReciveOrientationChangedResult', 1);
		}
		};
	},
	InitializePluginForDesktop: function () {
		window.onresize = function (event) {
		if(window.innerHeight > window.innerWidth){
			gameInstance.SendMessage('ScreenOrientationDetector', 'ReciveOrientationChangedResult', 0);
		}
		else{
			gameInstance.SendMessage('ScreenOrientationDetector', 'ReciveOrientationChangedResult', 1);
		}
		};
	},
});