var ScreenOpenerPlugin = {
  ScreenOpenerCaptureClick: function() {
    var OpenScreen = function() {
      parent.setFullscreen(parent.document.getElementById("gameContainer"));
      document.getElementById('#canvas').removeEventListener('mouseup', OpenScreen);
    };
    document.getElementById('#canvas').addEventListener('mouseup', OpenScreen, false);
  }
};
mergeInto(LibraryManager.library, ScreenOpenerPlugin);