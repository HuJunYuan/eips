/***

jQuery EasyUI 1.5
图标选择
必传入参数:表单Name
***/
$.parser.plugins.push("ico");
(function () {
    // 更改图标和表单值
    function setLinkbttnIconAndValue(target,param)
    {
        $(target).ico({
            iconCls: param,
            value: param
        });
    }
    function init(target) {
        var state = $.data(target, 'ico');
        var opts = state.options;
        $(target).textbox($.extend({}, opts, {
            onClickButton: function () {
                if (opts.onClick.call(target) == false) { return false };
                shoowIcoSelectDialog(target);
            }
        }));
        return $(target);
    };

    // 图标列初始话
    function shoowIcoSelectDialog(target) {
        var opts = $.data(target, 'ico').options;
        var iconUrl = $.util.hostPath + opts.iconUrl;
        $("#ico-select-dialog").dialog("destroy");
        var toolbarDiv = $('<div><input id="ico-select-searchbox" class="easyui-searchbox" style="width:300px" ></input></div>');
        var searchBox = toolbarDiv.find('#ico-select-searchbox')
        .searchbox({
            searcher: function (value, name) {
                doSerachKey(value);
            }
        });
        var dlg = $('<div id="ico-select-dialog" class="dialog-ico"></div>')
        .dialog({
            title: "图标选择",
            width: 555,
            height: 475,
            model: true,
            href: iconUrl,
            cache: false,
            parentTarget: target,
            toolbar: toolbarDiv
        });
        $.data(target, 'ico', { options: opts, dlg: dlg });
    };

    // 图标选择器：入口
    $.fn.ico = function (options, param) {
        if (typeof options == 'string') {
            var method = $.fn.ico.methods[options];
            if (method) {
                return method(this, param);
            } else {

                // 如果有自定义方法执行 没有 执行JQeasyUI原生方法
                return this.linkbutton(options, param);
            }
        }
        options = options || {};
        return this.each(function () {
            var state = $.data(this, 'ico');
            if (state) {

                // 如果已初始化,则更新属性
                $.extend(state.options, options); 
            } else {
                $.data(this, 'ico', {

                    // 获取dataoption参数
                    options: $.extend({}, $.fn.ico.defaults, $.fn.ico.parseOptions(this), options)
                });
            }
            init(this);
        });
    };

    $.fn.ico.methods = {
        options: function (jq) {
            return $.data(jq[0], 'ico').options;
        },
        SetIconCls: function (jq, param) {
            jq.each(function () {
              return  setLinkbttnIconAndValue(this,param);
            });
        }
    };

    $.fn.ico.parseOptions = function (target) {

        // 获取自定义杰点值
        return $.extend({}, $.fn.linkbutton.parseOptions(target), $.parser.parseOptions(target, ['iconUrl', 'Name', 'Value'])); 
    };

    $.fn.ico.defaults = $.extend({}, $.fn.linkbutton.defaults, {

        // 设置默认值
        buttonIcon: 'icon-search',
        iconUrl: '/EIP.Web/iconselect/iconselect',
        buttonAlign: 'right'
    });
})(jQuery);
