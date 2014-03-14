require([
        'jquery',
        'bforms-namespace',
        'bforms-ajax'
], function () {
    //#region Constructor and Properties
    var homeIndex = function (options) {
        this.options = $.extend(true, {}, options);
        this.init();
    };


    homeIndex.prototype.init = function () {
    };
    //#endregion



    //#region Dom Ready
    $(document).ready(function () {
        var page = new homeIndex(window.requireConfig.pageOptions);
    });
    //#endregion
});