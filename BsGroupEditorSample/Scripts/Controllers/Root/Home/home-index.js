require([
        'jquery',
        'bforms-namespace',
        'bforms-groupEditor',
        'bforms-initUI',
        'history-js',
        'bforms-ajax'
], function () {
    //#region Constructor and Properties
    var homeIndex = function (options) {
        this.options = $.extend(true, {}, options);
        this.init();
    };


    homeIndex.prototype.init = function () {
        $('#myGroupEditor').bsGroupEditor({
            getTabUrl: this.options.getTabUrl,
            buildDragHelper: function (model, tabId, connectsWith) {
                return $('<div class="col-lg-6 col-md-6 bs-itemContent" style="z-index:999"><span>' + model.DisplayNameInternational + '</span></div>');
            },
            buildGroupItem: $.proxy(function (model, group, tabId, objId) {
                return $('<span>' + model.DisplayNameInternational + '</span>');
            }, this),
            validateMove: function (model, tabId, $group) {
                if (model.Permissions == "BackEnd" && $group.data('groupid') == 1) return false;
                else if (model.Permissions == "BackEnd" && $group.data('groupid') == 2) return false;
                else if (model.Permissions == "FrontEnd" && $group.data('groupid') == 3) return false;
            },
            onSaveSuccess: $.proxy(function () {
            }, this),
            initEditorForm: $.proxy(function ($form, uid, tabModel) {

                if (uid == "2.Search") {
                   this._initSearchForm($form, uid);
                } else if (uid == "1.New") {
                   // this._initAddForm($form, uid);
                }


            }, this),
            validation: {
                required: {
                    unobtrusive: true,
                    message: "Please add at least an item."
                }
            }
        });
    };
    //#endregion


    //#region Dom Ready
    $(document).ready(function () {
        var page = new homeIndex(window.requireConfig.pageOptions.index);
    });
    //#endregion
});