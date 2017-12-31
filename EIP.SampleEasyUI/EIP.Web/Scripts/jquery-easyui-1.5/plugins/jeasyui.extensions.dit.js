/**
 * jQuery EasyUI 1.5
 * 
 * Copyright (c) 2009-2017 www.coreland.com.cn. All rights reserved.
 *
 * Licensed under the freeware license: http://www.jeasyui.com/license_freeware.php
 * To use it on other terms please contact us: info@jeasyui.com
 *
 */
/**
 * dit - jQuery EasyUI
 * 
 * Dependencies:
 * 	 人员树选择
 * 业务参数：
        ditIndentity: 人员树配置标识    默认值:'Sys.MultipleAll',
        title:        人员树选着框标题  默认值:'人员树选择',
        ditUrl:       人员树查询地址    默认值:'uucdit/dit',
        dptId:        指定根节点部门ID  默认值:无
        roleIndentity: 只显示指定角色标识名 默认值:无
   人员数返回值：
        CompanyCode	公司编码
        CompanyName 公司名称
        CompanyShortName 公司简称
        DptId	部门ID
        DepartmentName 部门显示名称
        DepartmentShortName 部门简称
        Struct 分层结构
        DN 分层显示名
        RelId   组织关系ID
        EmpId   人员ID
        EmployeeName    用户中文名
        Parttimer   兼职
        Position    职位（L1:职员，L2:开发组长，L3:项目经理，L4:部门经理等）
        PositionName 职位
        MangeLevel  管理职（9:无，1:正职，2:副职）
        MangeLevelName  管理职
 */
$.parser.plugins.push("uucdit");

(function ($) {

    function init(target) {
        var state = $.data(target, 'uucdit'),
            opts = state.options;

        if (opts.tagType == "tagbox") {
            $(target).tagbox($.extend({}, opts, {
                onClickButton: function () {
                    if (opts.onClickButton.call(target) == false) { return false };
                    showUUCDitDialog(target);
                }
            }));
            // label显示位置调整
            var tagobj = $(target).parent(),
                textbox = tagobj.find(".textbox"),
                textboxWidth = textbox.width(),
                lableWidth = tagobj.find(".textbox-label").width(),
                buttonWidth = textbox.find(".textbox-button").width();

            textbox.width(textboxWidth - lableWidth - buttonWidth + 15);

        } else {
            $(target).linkbutton($.extend({}, opts, {
                onClick: function () {
                    if (opts.onClick.call(target) == false) { return false };
                    showUUCDitDialog(target);
                }
            }));
        }
        return $(target);
    }

    

    function showUUCDitDialog(target) {
        var opts = $.data(target, 'uucdit').options;
        var ditUrl = $.util.hostPath + opts.ditUrl + "?DitIndentity=" + opts.ditIndentity;
        if (opts.dptId) {
            ditUrl += "&DptId=" + opts.dptId;
        }
        if (opts.roleIndentity) {
            ditUrl += "&RoleIndentity=" + opts.roleIndentity;
        }
        var dlg = $('<div class="dialog-uucdit"></div>')
        .dialog({
            title: opts.title,
            width: 555,
            height: 475,
            model: true,
            content: '<iframe class="content-frame" scrolling="no" frameborder="0"  src="' + ditUrl + '&_r='+Math.random()+'"></iframe>',
            buttons: [{
                text: '确定',
                iconCls: 'icon-ok',
                handler: function () {
                    var state = $.data(target, 'uucdit'),
                        opts = state.options,
                        dlg = state.dlg;
                    var returnValue = GetUUCDitSelected();
                    var ditOpts = GetUUCDitOptions();

                    if (!opts.valueField || opts.valueField == "") {
                        opts.valueField = ditOpts.valueField;
                    }

                    if (!opts.textField || opts.textField == "") {
                        opts.textField = ditOpts.textField;
                    }
                    
                    if (opts.onOkClick.call(target, returnValue) == false) { return false }

                    // 输入框自动设值
                    if (opts.tagType == "tagbox") {
                        var vals = $(target).uucdit("getValues") || [],
                            data = $(target).uucdit("getData") || [];
                        $.each(returnValue, function (index, row) {
                            if (!Array.contains(vals, row[opts.valueField])) {
                                vals.push(row[opts.valueField]);
                                data.push(row);
                            }
                        });

                        $(target).uucdit(opts);

                        $(target).uucdit("loadData", data).uucdit("setValues", vals);
                        //console.log(String.format("getText={0},getValues={1}", $(target).uucdit("getText"), $(target).uucdit("getValues")));
                    }

                    dlg.dialog("close");
                }
            }, {
                text: '取消',
                iconCls: 'icon-no',
                handler: function () {
                    var opts = $.data(target, 'uucdit').options;
                    if (opts.onCancelClick.call(target) == false) { return false }
                    dlg.dialog("close");
                }
            }]
        });

        $.data(target, 'uucdit', { options: opts, dlg: dlg });

    }

    function getText(target) {
        var state = $.data(target, 'combobox'),
            opts = state.options || {},
            data = state.data || [],
            vals = $(target).uucdit("getValues") || [],
            texts = [];
        $.each(data, function (index, row) {
            if (Array.contains(vals, row[opts.valueField])) {
                texts.push(row[opts.textField]);
            }
        });
        return texts.join(',');
    }

    $.fn.uucdit = function (options, param) {
        if (typeof options == 'string') {
            var method = $.fn.uucdit.methods[options];
            if (method) {
                return method(this, param);
            } else {
                return this.tagbox(options, param);
            }
        }

        options = options || {};
        return this.each(function () {
            var state = $.data(this, 'uucdit');
            if (state) {
                $.extend(state.options, options);
            } else {
                var opts = $.extend($.fn.uucdit.parseOptions(this), options);
                if (opts.tagType == "tagbox") {
                    $.data(this, 'uucdit', {
                        options: $.extend({}, $.fn.tagbox.defaults, $.fn.uucdit.defaults, opts)
                    });
                } else {
                    $.data(this, 'uucdit', {
                        options: $.extend({}, $.fn.linkbutton.defaults, $.fn.uucdit.defaults, opts)
                    });

                }
            }
            init(this);
        });
    };

    $.fn.uucdit.methods = {
        options: function (jq) {
            return $.data(jq[0], 'uucdit').options;
        },
        getText: function (jq) {
            return getText(jq[0]);
        }
    };

    $.fn.uucdit.parseOptions = function (target) {
        var opts = $.parser.parseOptions(target, ['tagType', 'valueField', 'textField', 'ditIndentity', 'title', 'ditUrl']);
        if (opts.tagType == "tagbox") {
            return $.extend({}, $.fn.tagbox.parseOptions(target), opts);
        } else {
            return $.extend({}, $.fn.linkbutton.parseOptions(target), opts);
        }
    };

    $.fn.uucdit.defaults = {
        tagType: 'linkbutton',
        ditIndentity: 'Sys.MultipleAll',
        valueField: '',
        textField: '',
        //buttonIcon: 'icon-user_magnify',
        //iconCls: 'icon-user_magnify',
        //iconAlign: 'left',
        title: '人员树选择',
        ditUrl: '/EIP.Web/uucdit/dit',
        onOkClick: function (returnValue) {  },
        onCancelClick: function () { }
    };

})(jQuery);
