/**
* jQuery EasyUI 1.5
* Copyright (c) 2009-2015 www.jeasyui.com. All rights reserved.
*
* Licensed under the GPL license: http://www.gnu.org/licenses/gpl.txt
* To use it on other terms please contact us at info@jeasyui.com
* http://www.jeasyui.com/license_commercial.php
*
* jQuery EasyUI form 扩展
* jeasyui.extensions.switchbutton.label.js
* 开发 Youke
* 由 Youke 整理
* 最近更新：2017-08-13
*
* 依赖项：
*   1、jquery.jdirk.js
*
* Copyright (c) 2017 coreland.com.cn All rights reserved.
*/
(function ($) {

    $.util.namespace("$.fn.switchbutton.extensions");

    function showLabel(target) {
        var state = $.data(target, "switchbutton");
        var options = state.options;
        var sb = state.switchbutton;
        var name = $(target).find(".switchbutton-value").attr("name");
        if (options.label) {
            $(target).find(".switchbutton-value").attr("id",name);
            if (typeof options.label == "object") {
                state.label = $(options.label);
                state.label.attr("for", name);
            } else {
                $(state.label).remove();
                state.label = $("<label class=\"textbox-label\"></label>").html(options.label);
                state.label.css("textAlign", options.labelAlign).attr("for", name);
                if (options.labelPosition == "after") {
                    state.label.insertAfter(sb);
                } else {
                    state.label.insertBefore(target);
                }
                state.label.removeClass("textbox-label-left textbox-label-right textbox-label-top");
                state.label.addClass("textbox-label-" + options.labelPosition);
            }
        } else {
            $(state.label).remove();
        }
    }


    var defaults = $.fn.switchbutton.extensions.defaults = {
        // The label position. Possible values are:'before','after','top'. 默认设置：'before'
        labelPosition: 'before',
        // The label alignment. Possible values are:'left','right'. 默认设置：'left'
        labelAlign: 'left'
    };

    var methods = $.fn.switchbutton.extensions.methods = {
        // 修改源代码，调用此代码
        extensionMethods: showLabel
    };


    $.extend($.fn.switchbutton.defaults, defaults);
    $.extend($.fn.switchbutton.methods, methods);


})(jQuery);