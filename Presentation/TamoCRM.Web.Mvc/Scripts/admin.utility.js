﻿// Extensions Format    
String.prototype.format = function () {
    var formatted = this;
    for (var i = 0; i < arguments.length; i++) {
        formatted = formatted.replace(RegExp('\\{' + i + '\\}', 'g'), arguments[i]);
    }
    return formatted;
};
String.prototype.subStringEx = function (length) {
    if (this == '') return '';
    if (this.length >= length) return this.substr(0, length) + "...";
    return this;
};
String.prototype.replaceAll = function (strTarget, strSubString) {
    var strText = this;
    if (strText.length == 0) return '';
    var intIndexOfMatch = strText.indexOf(strTarget);
    while (intIndexOfMatch != -1) {
        strText = strText.replace(strTarget, strSubString);
        intIndexOfMatch = strText.indexOf(strTarget);
    }
    return strText;
};

// Branch
var globalBranchs;
function bindGlobalBranchs() {
    if (!globalBranchs) {
        $.getJSON('/admin/api/Branch/get').done(function (data) {
            globalBranchs = data;
        });
    }
}
function branchFomatter(cellvalue, options, rowObject) {
    if (globalBranchs == null) {
        return cellvalue === undefined || cellvalue == null || cellvalue == 0 ? '' : cellvalue;
    }

    var retVal;
    $.each(globalBranchs, function (index) {

        if (globalBranchs[index].BranchId == cellvalue) {
            retVal = globalBranchs[index].Name;
        }
    });
    if (retVal) return retVal;
    return cellvalue === undefined || cellvalue == null || cellvalue == 0 ? '' : cellvalue;
}
function jqGridBranchDropVal(elem, operation, value) {
    if (operation === 'get') {
        return $(elem).val();
    } else if (operation === 'set') {
        $(elem).val(value);
    }
    return $(elem).val();
}
function jqGridBranchDropEl(value, options) {
    var retVal = $(document.createElement('select'));
    $(retVal).addClass("form-control");

    $.each(globalBranchs, function (index) {
        if (globalBranchs[index].Name == value) {
            $(retVal).append("<option selected value='" + globalBranchs[index].BranchId + "'>" + globalBranchs[index].Name + "</option>");
        } else {
            $(retVal).append("<option value='" + globalBranchs[index].BranchId + "'>" + globalBranchs[index].Name + "</option>");
        }
    });
    //});
    return retVal;
}

var globalWebServiceTypes;
function webServiceTypeFomatter(cellvalue, options, rowObject) {
    if (globalWebServiceTypes == null) {
        return cellvalue === undefined || cellvalue == null || cellvalue == 0 ? '' : cellvalue;
    }

    var retVal;
    $.each(globalWebServiceTypes, function (index) {

        if (globalWebServiceTypes[index].Id == cellvalue) {
            retVal = globalWebServiceTypes[index].Name;
        }
    });
    if (retVal) return retVal;
    return cellvalue === undefined || cellvalue == null || cellvalue == 0 ? '' : cellvalue;
}
function jqGridWebServiceTypeDropVal(elem, operation, value) {
    if (operation === 'get') {
        return $(elem).val();
    } else if (operation === 'set') {
        $(elem).val(value);
    }
    return $(elem).val();
}
function jqGridWebServiceTypeDropEl(value, options) {
    var retVal = $(document.createElement('select'));
    $(retVal).addClass("form-control");

    $.each(globalWebServiceTypes, function (index) {
        if (globalWebServiceTypes[index].Name == value) {
            $(retVal).append("<option selected value='" + globalWebServiceTypes[index].Id + "'>" + globalWebServiceTypes[index].Name + "</option>");
        } else {
            $(retVal).append("<option value='" + globalWebServiceTypes[index].Id + "'>" + globalWebServiceTypes[index].Name + "</option>");
        }
    });
    //});
    return retVal;
}

// User
var globalUsers;
function bindGlobalUsers() {
    if (globalUsers == null) {
        $.getJSON('/admin/api/user/get').done(function (data) {
            globalUsers = data;
        });
    }
}
function buildParamUsers() {
    var param = '';
    if (globalUsers != null) {
        for (var i = 0; i < globalUsers.length; i++) {
            var id = globalUsers[i].UserID;
            if (!$('#' + id + '_chkUser').is(':checked')) continue;
            param += param.length == 0 ? id : ',' + id;
        }
    }
    return param;
}
function userFomatter(cellvalue, options, rowObject) {
    if (globalUsers == null) {
        return cellvalue === undefined || cellvalue == null || cellvalue == 0 ? '' : cellvalue;
    }

    var retVal;
    $.each(globalUsers, function (index) {

        if (globalUsers[index].UserID == cellvalue) {
            retVal = globalUsers[index].FullName;
        }
    });
    if (retVal) return retVal;
    return cellvalue === undefined || cellvalue == 0 ? '' : cellvalue;
}

//Groups
var globalGroups;
function bindGlobalGroups() {
    
    if (!globalGroups) {
        $.getJSON('/admin/api/group/get').done(function (data) {
            globalGroups = data;
        })
    }
    
}
function buildParamGroups() {
    var param = '';
    var count = 0;
    if (globalGroups != null) {
        for (var i = 0; i < globalGroups.length; i++) {
            var id = globalGroups[i].GroupId;
            if (!$('#' + id + '_chkGroups').is(':checked')) continue;
            count++;
            param += param.length == 0 ? id : ',' + id;
        }
    }
    
    if (count > 1)
    {
        $("#UserId").prop("disabled", true);
        $("#UserId").val(0);
    }
    else {
        $("#UserId").prop("disabled", false);
    }
    return param;
}

// Level
var globalLevels;
function bindGlobalLevels() {
    if (!globalLevels) {
        $.getJSON('/admin/api/level/get').done(function (data) {
            globalLevels = data;
        });
    }
}
function buildParamLevels() {
    var param = '';
    if (globalLevels != null) {
        for (var i = 0; i < globalLevels.length; i++) {
            var id = globalLevels[i].LevelId;
            if (!$('#' + id + '_chkLevel').is(':checked')) continue;
            param += param.length == 0 ? id : ',' + id;
        }
    }
    return param;
}
function levelFomatter(cellvalue, options, rowObject) {
    if (globalLevels == null) {
        return cellvalue === undefined || cellvalue == null ? '' : cellvalue;
    }
    if (cellvalue == 0) cellvalue = 1;
    var retVal;
    $.each(globalLevels, function (index) {

        if (globalLevels[index].LevelId == cellvalue) {
            retVal = globalLevels[index].Name;
        }
    });
    if (retVal) return retVal;
    return cellvalue === undefined || cellvalue == null ? '' : cellvalue;
}
function levelUnformat(cellvalue, options) {
    //console.log(cellvalue);
    $.each(globalLevels, function (index) {
        if (globalLevels[index].Name == cellvalue) {
            return globalLevels[index].LevelId;
        }
    });
    return cellvalue;
}
function jqGridLevelDropVal(elem, operation, value) {
    if (operation === 'get') {
        return $(elem).val();
    } else if (operation === 'set') {
        $(elem).val(value);
    }
};
function jqGridLevelDropEl(value, options) {
    var retVal = $(document.createElement('select'));
    $(retVal).addClass("form-control");

    $.each(globalLevels, function (index) {
        if (globalLevels[index].LevelId == value) {
            $(retVal).append("<option selected value='" + globalLevels[index].LevelId + "'>" + globalLevels[index].Name + "</option>");
        } else {
            $(retVal).append("<option value='" + globalLevels[index].LevelId + "'>" + globalLevels[index].Name + "</option>");
        }
    });
    return retVal;
}

// Major
var globalMajors;
function bindGlobalMajors() {
    if (!globalMajors) {
        $.getJSON('/admin/api/major/get').done(function (data) {
            globalMajors = data;
        });
    }
}
function buildParamMajors() {
    var param = '';
    if (globalMajors != null) {
        for (var i = 0; i < globalMajors.length; i++) {
            var id = globalMajors[i].MajorId;
            if (!$('#' + id + '_chkMajor').is(':checked')) continue;
            param += param.length == 0 ? id : ',' + id;
        }
    }
    return param;
}
function majorFomatter(cellvalue, options, rowObject) {
    if (globalMajors == null) {
        return cellvalue === undefined || cellvalue == null || cellvalue == 0 ? '' : cellvalue;
    }

    var retVal;
    $.each(globalMajors, function (index) {

        if (globalMajors[index].MajorId == cellvalue) {
            retVal = globalMajors[index].Name;
        }
    });
    if (retVal) return retVal;
    return cellvalue === undefined || cellvalue == null || cellvalue == 0 ? '' : cellvalue;
}

// Location
var globalLocations;
function bindGlobalLocations() {
    if (!globalLocations) {
        $.getJSON('/admin/api/location/get').done(function (data) {
            globalLocations = data;
        });
    }
}
function locationFomatter(cellvalue, options, rowObject) {
    if (globalLocations == null) {
        return cellvalue === undefined || cellvalue == null || cellvalue == 0 ? '' : cellvalue;
    }

    var retVal;
    $.each(globalLocations, function (index) {

        if (globalLocations[index].LocationId == cellvalue) {
            retVal = globalLocations[index].Name;
        }
    });
    if (retVal) return retVal;
    return cellvalue === undefined || cellvalue == null || cellvalue == 0 ? '' : cellvalue;
}

// Status
var globalStatus;
function bindGlobalStatus() {
    if (!globalStatus) {
        $.getJSON('/admin/api/status/get').done(function (data) {
            globalStatus = data;
        });
    }
}
function buildParamStatus() {
    var param = '';
    if (globalStatus != null) {
        for (var i = 0; i < globalStatus.length; i++) {
            var id = globalStatus[i].Id;
            if (!$('#' + id + '_chkStatus').is(':checked')) continue;
            param += param.length == 0 ? id : ',' + id;
        }
    }
    return param;
}
function statusFomatter(cellvalue, options, rowObject) {
    if (globalStatus == null) {
        return cellvalue === undefined || cellvalue == null || cellvalue == 0 ? '' : cellvalue;
    }

    var retVal;
    $.each(globalStatus, function (index) {

        if (globalStatus[index].Id == cellvalue) {
            retVal = globalStatus[index].Name;
        }
    });
    if (retVal) return retVal;
    return cellvalue === undefined || cellvalue == null || cellvalue == 0 ? '' : cellvalue;
}

// Collectors
var globalCollectors;
function collectorFomatter(cellvalue, options, rowObject) {
    if (globalCollectors == null) {
        return cellvalue === undefined || cellvalue == null || cellvalue == 0 ? '' : cellvalue;
    }

    var retVal;
    $.each(globalCollectors, function (index) {

        if (globalCollectors[index].CollectorId == cellvalue) {
            retVal = globalCollectors[index].Name;
        }
    });
    if (retVal) return retVal;
    return cellvalue === undefined || cellvalue == null || cellvalue == 0 ? '' : cellvalue;
}

// StatusMaps
var globalStatusMaps;
function bindGlobalStatusMaps() {
    if (!globalStatusMaps) {
        $.getJSON('/admin/api/statusMap/get').done(function (data) {
            globalStatusMaps = data;
        });
    }
}
function buildParamStatusMaps() {
    var param = '';
    if (globalStatusMaps != null) {
        for (var i = 0; i < globalStatusMaps.length; i++) {
            var id = globalStatusMaps[i].Id;
            if (!$('#' + id + '_chkStatusMap').is(':checked')) continue;
            param += param.length == 0 ? id : ',' + id;
        }
    }
    return param;
}
function statusMapFomatter(cellvalue, options, rowObject) {
    if (globalStatusMaps == null) {
        return cellvalue === undefined || cellvalue == null || cellvalue == 0 ? '' : cellvalue;
    }

    var retVal;
    $.each(globalStatusMaps, function (index) {

        if (globalStatusMaps[index].Id == cellvalue) {
            retVal = globalStatusMaps[index].Name;
        }
    });
    if (retVal) return retVal;
    return cellvalue === undefined || cellvalue == null || cellvalue == 0 ? '' : cellvalue;
}
function jqGridStatusMapDropVal(elem, operation, value) {
    if (operation === 'get') {
        return $(elem).val();
    } else if (operation === 'set') {
        $(elem).val(value);
    }
};
function jqGridStatusMapDropEl(value, options) {
    var retVal = $(document.createElement('select'));
    //retval.innerHTML = "<option value='1'>HN</option>";
    $(retVal).addClass("form-control");

    $.each(globalStatusMaps, function (index) {
        if (globalStatusMaps[index].Id == value) {
            $(retVal).append("<option selected value='" + globalStatusMaps[index].Id + "'>" + globalStatusMaps[index].Name + "</option>");
        } else {
            $(retVal).append("<option value='" + globalStatusMaps[index].Id + "'>" + globalStatusMaps[index].Name + "</option>");
        }
    });
    //});
    return retVal;
}

// StatusCares
var globalStatusCares;
function bindGlobalStatusCares() {
    if (!globalStatusCares) {
        $.getJSON('/admin/api/statusCare/get').done(function (data) {
            globalStatusCares = data;
        });
    }
}
function buildParamStatusCares() {
    var param = '';
    if (globalStatusCares != null) {
        for (var i = 0; i < globalStatusCares.length; i++) {
            var id = globalStatusCares[i].Id;
            if (!$('#' + id + '_chkStatusCare').is(':checked')) continue;
            param += param.length == 0 ? id : ',' + id;
        }
    }
    return param;
}
function statusCareFomatter(cellvalue, options, rowObject) {
    if (globalStatusCares == null) {
        return cellvalue === undefined || cellvalue == null || cellvalue == 0 ? '' : cellvalue;
    }

    var retVal;
    $.each(globalStatusCares, function (index) {

        if (globalStatusCares[index].Id == cellvalue) {
            retVal = globalStatusCares[index].Name;
        }
    });
    if (retVal) return retVal;
    return cellvalue === undefined || cellvalue == null || cellvalue == 0 ? '' : cellvalue;
}

// Products
var globalProducts;
function bindGlobalProducts() {
    if (!globalProducts) {
        $.getJSON('/admin/api/Product/get').done(function (data) {
            globalProducts = data;
        });
    }
}

function productFomatter(cellvalue, options, rowObject) {

    if (globalProducts == null) {
        alert("global null");
        return cellvalue === undefined || cellvalue == null || cellvalue == 0 ? '' : cellvalue;
    }
    
    var retVal;
    $.each(globalProducts, function (index) {
        if (globalProducts[index].Id == cellvalue) {
            retVal = globalProducts[index].Name;
        }
    });
    
    if (retVal) return retVal;
    
    return cellvalue === undefined || cellvalue == null || cellvalue == 0 ? '' : cellvalue;

}

//Format định dạng tiền (12,000,000)
function moneyFormatter(cellvalue, options, rowObject) {
    if (cellvalue == null || cellvalue.length == 0) return '';
    while (/(\d+)(\d{3})/.test(cellvalue.toString())) {
        cellvalue = cellvalue.toString().replace(/(\d+)(\d{3})/, '$1' + ',' + '$2');
    }
    return cellvalue;
}

// School
var globalSchools;
function bindGlobalSchools() {
    if (!globalSchools) {
        $.getJSON('/admin/api/school/get').done(function (data) {
            globalSchools = data;
        });
    }
}
function buildParamSchools() {
    var param = '';
    if (globalSchools != null) {
        for (var i = 0; i < globalSchools.length; i++) {
            var id = globalSchools[i].SchoolId;
            if (!$('#' + id + '_chkSchool').is(':checked')) continue;
            param += param.length == 0 ? id : ',' + id;
        }
    }
    return param;
}
function schoolFomatter(cellvalue, options, rowObject) {
    if (globalSchools == null) {
        return cellvalue === undefined || cellvalue == null || cellvalue == 0 ? '' : cellvalue;
    }

    var retVal;
    $.each(globalSchools, function (index) {

        if (globalSchools[index].SchoolId == cellvalue) {
            retVal = globalSchools[index].Name;
        }
    });
    if (retVal) return retVal;
    return cellvalue === undefined || cellvalue == null || cellvalue == 0 ? '' : cellvalue;
}

// Channel
var globalChannels;
function bindGlobalChannels() {
    if (!globalChannels) {
        $.getJSON('/admin/api/channel/get').done(function (data) {
            globalChannels = data;
        });
    }
}
function buildParamChannels() {
    var param = '';
    if (globalChannels != null) {
        for (var i = 0; i < globalChannels.length; i++) {
            var id = globalChannels[i].ChannelId;
            if (!$('#' + id + '_chkChannel').is(':checked')) continue;
            param += param.length == 0 ? id : ',' + id;
        }
    }
    return param;
}

function buildParamChannel(Channels) {
    var param = '';
    if (Channels != null) {
        for (var i = 0; i < Channels.length; i++) {
            var id = Channels[i].ChannelId;
            if (!$('#' + id + '_chkChannel').is(':checked')) continue;
            param += param.length == 0 ? id : ',' + id;
        }
    }
    return param;
}

function buildParamChannelAmounts() {
    var param = '';
    if (globalChannels != null) {
        for (var i = 0; i < globalChannels.length; i++) {
            var id = globalChannels[i].ChannelId;
            if (!$('#' + id + '_chkChannel').is(':checked')) continue;

            var amount = $('#' + id + '_txtChannelAmount').val();
            param += param.length == 0 ? amount : ',' + amount;
        }
    }
    return param;
}

function buildParamChannelAmount(ChannelAmounts) {
    var param = '';
    if (ChannelAmounts != null) {
        for (var i = 0; i < ChannelAmounts.length; i++) {
            var id = ChannelAmounts[i].ChannelId;
            if (!$('#' + id + '_chkChannel').is(':checked')) continue;
            var amount = $('#' + id + '_txtChannelAmount').val();
            param += param.length == 0 ? amount : ',' + amount;
        }
    }
    return param;
}

//Ham tra chuoi bao gom so cac contact / tung kenh, voi tham so dau vao la ty le phan tram so contact tren 1 kenh
function buildParamChannelPercentageAmount(ChannelPercentageAmount) 
{
    var param = '';
    if (ChannelPercentageAmount != null) {
        for (var i = 0; i < ChannelPercentageAmount.length; i++) {
            var id = ChannelPercentageAmount[i].ChannelId;
            if (!$('#' + id + '_chkChannel').is(':checked'))
                continue;
            var percentage = $('#' + id + '_txtChannelAmount').val();
            var amount = parseInt($('.' + id + '_lblChannelCount').val()) / 100 * parseInt($('#' + id + '_txtChannelAmount').val());
            param += param.length == 0 ? amount : ',' + amount;
        }
    }
}

function channelFomatter(cellvalue, options, rowObject) {
    
    if (globalChannels == null) {
        return cellvalue === undefined || cellvalue == null || cellvalue == 0 ? '' : cellvalue;
    }
    
    var retVal;
    $.each(globalChannels, function (index) {
        if (globalChannels[index].ChannelId == cellvalue) {
            retVal = globalChannels[index].Name;
            
        }
    });
    if (retVal) return retVal;
    return cellvalue === undefined || cellvalue == null || cellvalue == 0 ? '' : cellvalue;
}

// SourceType
var globalSourceTypes;
function bindGlobalSourceTypes() {
    if (!globalSourceTypes) {
        $.getJSON('/admin/api/sourcetype/get').done(function (data) {
            globalSourceTypes = data;
        });
    }
}
function buildParamSourceTypes() {
    var param = '';
    if (globalSourceTypes != null) {
        for (var i = 0; i < globalSourceTypes.length; i++) {
            var id = globalSourceTypes[i].SourceTypeId;
            if (!$('#' + id + '_chkSourceType').is(':checked')) continue;
            param += param.length == 0 ? id : ',' + id;
        }
    }
    return param;
}
function jqGridSourceTypeDropEl(value, options) {
    var retVal = $(document.createElement('select'));
    $(retVal).addClass("form-control");

    $.each(globalSourceTypes, function (index) {
        {
            if (globalSourceTypes[index].Name == value) {
                $(retVal).append("<option selected value='" + globalSourceTypes[index].SourceTypeId + "'>" + globalSourceTypes[index].Name + "</option>");
            } else {
                $(retVal).append("<option value='" + globalSourceTypes[index].SourceTypeId + "'>" + globalSourceTypes[index].Name + "</option>");
            }
        }
    });
    //});
    return retVal;
}
function jqGridSourceTypeDropVal(elem, operation, value) {
    if (operation === 'get') {
        return $(elem).val();
    } else if (operation === 'set') {
        $(elem).val(value);
    }
    return $(elem).val();
}
function sourceTypeFomatter(cellvalue, options, rowObject) {
    if (globalSourceTypes == null) {
        return cellvalue === undefined || cellvalue == null || cellvalue == 0 ? '' : cellvalue;
    }

    var retVal;
    $.each(globalSourceTypes, function (index) {

        if (globalSourceTypes[index].SourceTypeId == cellvalue) {
            retVal = globalSourceTypes[index].Name;
        }
    });
    if (retVal) return retVal;
    return cellvalue === undefined || cellvalue == null || cellvalue == 0 ? '' : cellvalue;
}

// StatusCampainType
var globalStatusCampainTypes;
function statusCampainTypeFomatter(cellvalue, options, rowObject) {
    if (globalStatusCampainTypes == null) {
        return cellvalue === undefined || cellvalue == null || cellvalue == 0 ? '' : cellvalue;
    }

    var retVal;
    $.each(globalStatusCampainTypes, function (index) {

        if (globalStatusCampainTypes[index].Id == cellvalue) {
            retVal = globalStatusCampainTypes[index].Name;
        }
    });
    if (retVal) return retVal;
    return cellvalue === undefined || cellvalue == null || cellvalue == 0 ? '' : cellvalue;
}

// EducationLevel
var globalEducationLevels;
function bindGlobalEducationLevels() {
    if (!globalEducationLevels) {
        $.getJSON('/admin/api/educationlevel/get').done(function (data) {
            globalEducationLevels = data;
        });
    }
}
function buildParamEducationLevels() {
    var param = '';
    if (globalEducationLevels != null) {
        for (var i = 0; i < globalEducationLevels.length; i++) {
            var id = globalEducationLevels[i].EducationLevelId;
            if (!$('#' + id + '_chkEducationLevel').is(':checked')) continue;
            param += param.length == 0 ? id : ',' + id;
        }
    }
    return param;
}
function educationLevelFomatter(cellvalue, options, rowObject) {
    if (globalEducationLevels == null) {
        return cellvalue === undefined || cellvalue == null || cellvalue == 0 ? '' : cellvalue;
    }

    var retVal;
    $.each(globalEducationLevels, function (index) {

        if (globalEducationLevels[index].EducationLevelId == cellvalue) {
            //console.log(globalStatusMap[index].Name);
            retVal = globalEducationLevels[index].Name;
        }
    });
    if (retVal) return retVal;
    return cellvalue === undefined || cellvalue == null || cellvalue == 0 ? '' : cellvalue;
}
function educationLevelUnformat(cellvalue, options) {
    //console.log(cellvalue);
    $.each(globalEducationLevels, function (index) {
        if (globalEducationLevels[index].Name == cellvalue) {
            return globalEducationLevels[index].EducationLevelId;
        }
    });
    return cellvalue;
}
function jqGridEducationLevelDropVal(elem, operation, value) {
    if (operation === 'get') {
        return $(elem).val();
    } else if (operation === 'set') {
        $(elem).val(value);
    }
};
function jqGridEducationLevelDropEl(value, options) {
    var retVal = $(document.createElement('select'));
    //retval.innerHTML = "<option value='1'>HN</option>";
    $(retVal).addClass("form-control");

    $.each(globalEducationLevels, function (index) {
        if (globalEducationLevels[index].EducationLevelId == value) {
            $(retVal).append("<option selected value='" + globalEducationLevels[index].EducationLevelId + "'>" + globalEducationLevels[index].Name + "</option>");
        } else {
            $(retVal).append("<option value='" + globalEducationLevels[index].EducationLevelId + "'>" + globalEducationLevels[index].Name + "</option>");
        }
    });
    //});
    return retVal;
}

// Lumped
var globalLumpeds;
function bindGlobalLumpeds() {
    if (!globalLumpeds) {
        $.getJSON('/admin/api/lumped/get').done(function (data) {
            globalLumpeds = data;
        });
    }
}
function lumpedFomatter(cellvalue, options, rowObject) {
    if (globalLumpeds == null) {
        return cellvalue === undefined || cellvalue == null || cellvalue == 0 ? '' : cellvalue;
    }

    var retVal;
    $.each(globalLumpeds, function (index) {

        if (globalLumpeds[index].Id == cellvalue) {
            retVal = globalLumpeds[index].Name;
        }
    });
    if (retVal) return retVal;
    return cellvalue === undefined || cellvalue == null || cellvalue == 0 ? '' : cellvalue;
}
function jqGridLumpedDropVal(elem, operation, value) {
    if (operation === 'get') {
        return $(elem).val();
    } else if (operation === 'set') {
        $(elem).val(value);
    }
    return $(elem).val();
}
function jqGridLumpedDropEl(value, options) {
    var retVal = $(document.createElement('select'));
    $(retVal).addClass("form-control");

    $.each(globalLumpeds, function (index) {
        if (globalLumpeds[index].Name == value) {
            $(retVal).append("<option selected value='" + globalLumpeds[index].Id + "'>" + globalLumpeds[index].Name + "</option>");
        } else {
            $(retVal).append("<option value='" + globalLumpeds[index].Id + "'>" + globalLumpeds[index].Name + "</option>");
        }
    });
    //});
    return retVal;
}

// HaveJob
function haveJobFomatter(cellvalue, options, rowObject) {
    return cellvalue === undefined ||
        cellvalue == null ||
        cellvalue == 0 ||
        cellvalue == false ? 'Chưa' : 'Rồi';
}

// Enum UserLeader
var globalUserLeaders;
function userLeaderFomatter(cellvalue, options, rowObject) {
    if (globalUserLeaders == null) {
        return cellvalue === undefined || cellvalue == null || cellvalue == 0 ? '' : cellvalue;
    }

    var retVal;
    $.each(globalUserLeaders, function (index) {

        if (globalUserLeaders[index].Id == cellvalue) {
            retVal = globalUserLeaders[index].Name;
        }
    });
    if (retVal) return retVal;
    return cellvalue === undefined || cellvalue == null || cellvalue == 0 ? '' : cellvalue;
}
function jqGridUserLeaderDropVal(elem, operation, value) {
    if (operation === 'get') {
        return $(elem).val();
    } else if (operation === 'set') {
        $(elem).val(value);
    }
    return $(elem).val();
}
function jqGridUserLeaderDropEl(value, options) {
    var groupId = options.id.split('_')[0];
    var retVal = $(document.createElement('select'));
    $(retVal).addClass("form-control");

    $.each(globalUserLeaders, function (index) {
        {
            if (globalUserLeaders[index].Id == value) {
                $(retVal).append("<option selected value='" + globalUserLeaders[index].Id + "'>" + globalUserLeaders[index].Name + "</option>");
            } else {
                $(retVal).append("<option value='" + globalUserLeaders[index].Id + "'>" + globalUserLeaders[index].Name + "</option>");
            }
        }
    });
    return retVal;
}

// Enum EmployeeType
var globalEmployeeTypes;
function employeeTypeFomatter(cellvalue, options, rowObject) {
    if (globalEmployeeTypes == null) {
        return cellvalue === undefined || cellvalue == null || cellvalue == 0 ? '' : cellvalue;
    }

    var retVal;
    $.each(globalEmployeeTypes, function (index) {

        if (globalEmployeeTypes[index].Id == cellvalue) {
            retVal = globalEmployeeTypes[index].Name;
        }
    });
    if (retVal) return retVal;
    return cellvalue === undefined || cellvalue == null || cellvalue == 0 ? '' : cellvalue;
}
function jqGridEmployeeTypeDropVal(elem, operation, value) {
    if (operation === 'get') {
        return $(elem).val();
    } else if (operation === 'set') {
        $(elem).val(value);
    }
    return $(elem).val();
}
function jqGridEmployeeTypeDropEl(value, options) {
    var retVal = $(document.createElement('select'));
    $(retVal).addClass("form-control");

    $.each(globalEmployeeTypes, function (index) {
        {
            if (globalEmployeeTypes[index].Name == value) {
                $(retVal).append("<option selected value='" + globalEmployeeTypes[index].Id + "'>" + globalEmployeeTypes[index].Name + "</option>");
            } else {
                $(retVal).append("<option value='" + globalEmployeeTypes[index].Id + "'>" + globalEmployeeTypes[index].Name + "</option>");
            }
        }
    });
    return retVal;
}

// ConnectStatus
var globalStatusConnectTypes;
function connectStatusFomatter(cellvalue, options, rowObject) {
    if (globalStatusConnectTypes == null) {
        return cellvalue === undefined || cellvalue == null || cellvalue == 0 ? '' : cellvalue;
    }

    var retVal;
    $.each(globalStatusConnectTypes, function (index) {

        if (globalStatusConnectTypes[index].Id == cellvalue) {
            retVal = globalStatusConnectTypes[index].Name;
        }
    });
    if (retVal) return retVal;
    return cellvalue === undefined || cellvalue == null || cellvalue == 0 ? '' : cellvalue;
}

function haveJobFormatter(cellvalue, options, rowObject) {
    return cellvalue === undefined || cellvalue == null || cellvalue == 0 ? 'Chưa' : 'Rồi';
}

function haveLearnFormatter(cellvalue, options, rowObject) {
    return cellvalue === undefined || cellvalue == null || cellvalue == 0 ? 'Chưa' : 'Rồi';
}

function stringFormatter(cellvalue, options, rowObject) {
    return cellvalue === undefined || cellvalue == null ? '' : cellvalue;
}

function listenFomatter(cellvalue, options, rowObject) {
    if (cellvalue == null || cellvalue == '')
        return '';
    //return "<audio id='audio_listen' preload='none' controls style='width: 98%'> <source src='" + cellvalue + "' type='audio/mpeg' /></audio>";
    return "<div id='ngheghiam_" + rowObject.CallHistoryId + "' onclick='PlayRecord(" + rowObject.CallHistoryId + ")' style='cursor: pointer'>Nghe ghi âm<input type='hidden' id='link_record_" + rowObject.CallHistoryId + "' value='" + cellvalue + "' /></div>";
}

// Checkbox
function checkboxFomatter(cellvalue, options, rowObject) {
    return "<input type='checkbox' name='checkbox'>";
}

function isCloseFomatter(cellvalue, options, rowObject) {
    return cellvalue == true ? "Đóng" : "Mở";
}

(function ($) {
    'use strict';
    /*jslint unparam: true */
    $.extend($.fn.fmatter, {
        dynamicLink: function (cellValue, options, rowData) {
            // href, target, rel, title, onclick
            // other attributes like media, hreflang, type are not supported currently
            var op = { url: '#' };

            if (typeof options.colModel.formatoptions !== 'undefined') {
                op = $.extend({}, op, options.colModel.formatoptions);
            }
            if ($.isFunction(op.target)) {
                op.target = op.target.call(this, cellValue, options.rowId, rowData, options);
            }
            if ($.isFunction(op.url)) {
                op.url = op.url.call(this, cellValue, options.rowId, rowData, options);
            }
            if ($.isFunction(op.cellValue)) {
                cellValue = op.cellValue.call(this, cellValue, options.rowId, rowData, options);
            }
            if ($.fmatter.isString(cellValue) || $.fmatter.isNumber(cellValue)) {
                return '<a' +
                    (op.target ? ' target=' + op.target : '') +
                    (op.onClick ? ' onclick="return $.fn.fmatter.dynamicLink.onClick.call(this, arguments[0]);"' : '') +
                    ' href="' + op.url + '">' +
                    (cellValue || '&nbsp;') + '</a>';
            } else {
                return '&nbsp;';
            }
        }
    });
    $.extend($.fn.fmatter.dynamicLink, {
        unformat: function (cellValue, options, elem) {
            var text = $(elem).text();
            return text === '&nbsp;' ? '' : text;
        },
        onClick: function (e) {
            var $cell = $(this).closest('td'),
                $row = $cell.closest('tr.jqgrow'),
                $grid = $row.closest('table.ui-jqgrid-btable'),
                p,
                colModel,
                iCol;

            if ($grid.length === 1) {
                p = $grid[0].p;
                if (p) {
                    iCol = $.jgrid.getCellIndex($cell[0]);
                    colModel = p.colModel;
                    colModel[iCol].formatoptions.onClick.call($grid[0],
                        $row.attr('id'), $row[0].rowIndex, iCol, $cell.text(), e);
                }
            }
            return false;
        }
    });
}(jQuery));

// Custom validate
function customValidate() {
    var elements = document.getElementsByTagName('INPUT');
    for (var i = 0; i < elements.length; i++) {
        elements[i].oninvalid = function (e) {
            e.target.setCustomValidity('');
            if (!e.target.validity.valid) {
                e.target.setCustomValidity(e.target.getAttribute('error'));
            }
        };
    }
}

function alertProcess() {
    $("#alert").html('');
    $("#alert").removeClass();
    $("#alert").addClass("alert alert-block alert-info");
    $("#alert").html("<i class='icon-spinner icon-spin orange bigger-125'></i>&nbsp;Đang xử lý...");
    document.body.scrollTop = document.documentElement.scrollTop = 0;
    
}

function alertError(message) {
    $("#alert").html('');
    $("#alert").removeClass();
    $("#alert").html(message);
    $("#alert").addClass("alert alert-dismissable alert-warning");
    document.body.scrollTop = document.documentElement.scrollTop = 0;
};

function alertSuccess(message) {
    $("#alert").html('');
    $("#alert").removeClass();
    $("#alert").html(message);
    $("#alert").addClass("alert alert-block alert-success");
    document.body.scrollTop = document.documentElement.scrollTop = 0;
};

function alertHide(message) {
    $("#alert").html('');
    $("#alert").removeClass();
};

function checkAll(chkAll, chkItemName) {
    var elements = document.getElementsByName(chkItemName);
    for (var i = 0; i < elements.length; i++) {
        $(elements[i]).prop('checked', $('#' + chkAll).is(":checked"));
    }
}

function checkAllClassName(chkAll, chkClassName) {   
    var elements = document.getElementsByClassName(chkClassName);   
    for (var i = 0; i < elements.length; i++) {       
        $(elements[i]).prop('checked', $('#' + chkAll).is(":checked"));
        $(elements[i]).change();
    }
}

function fixCheckBoxGrid(e) {
    e = e || event;/* get IE event ( not passed ) */
    e.stopPropagation ? e.stopPropagation() : e.cancelBubble = true;
}

function loadStatusMap(employeeTypeId) {
    
    $('#ddlStatusMap').empty();
    var statusCareId = $("#ddlStatusCare").val();
    var url = "/admin/api/statusmap/GetByStatusCareId?statusCareId=" + statusCareId + '&employeeTypeId=' + employeeTypeId;
    $.ajax({
        url: url,
        type: 'GET',
        success: function (data) {
            if (data == null) return;
            var options = '';
            for (var i = 0; i < data.length; i++) {
                options += '<option value="' + data[i].Id + '">L' + data[i].LevelId + ' - ' + data[i].Name + '</option>';

            }
            $('#ddlStatusMap').empty().append(options);
        }
    });
}

function loadStatusMapKhac(employeeTypeId) {   //Viet them cho 
    
    $('#StatusMapId').empty();
    var statusCareId = $("#StatusCareId").val();
    var url = "/admin/api/statusmap/GetByStatusCareId?statusCareId=" + statusCareId + '&employeeTypeId=' + employeeTypeId;
    $.ajax({
        url: url,
        type: 'GET',
        success: function (data) {
            if (data == null) return;
            var options = '';
            for (var i = 0; i < data.length; i++) {
                options += '<option value="' + data[i].Id + '">L' + data[i].LevelId + ' - ' + data[i].Name + '</option>';

            }
            $('#StatusMapId').empty().append(options);
        }
    });
}

function loadStatusMapByLevel(levelIds, employeeTypeId) {
    $('#ddlStatusMap').empty();
    var statusCareId = $("#ddlStatusCare").val();
    var url = "/admin/api/statusmap/GetByStatusCareAndLevel?statusCareId={0}&levelIds={1}&employeeTypeId={2}";
    url = url.format(statusCareId, levelIds, employeeTypeId);
    $.ajax({
        url: url,
        type: 'GET',
        success: function (data) {
            if (data == null) return;
            var options = '';
            for (var i = 0; i < data.length; i++) {
                options += '<option value="' + data[i].Id + '">L' + data[i].LevelId + ' - ' + data[i].Name + '</option>';
            }
            $('#ddlStatusMap').empty().append(options);
        }
    });
}

//GetByStatusCareId

function loadStatusMapForHandoverContact(levelIds, employeeTypeId) {
    $('#StatusMapId').empty();
    var statusCareId = $("#StatusCareId").val();
    var url = "/admin/api/statusmap/GetByStatusCareId?statusCareId={0}&employeeTypeId={1}";
    url = url.format(statusCareId, employeeTypeId);
    $.ajax({
        url: url,
        type: 'GET',
        success: function (data) {
            if (data == null) return;
            var options = '';
            for (var i = 0; i < data.length; i++) {
                options += '<option value="' + data[i].Id + '">L' + data[i].LevelId + ' - ' + data[i].Name + '</option>';
            }
            $('#StatusMapId').empty().append(options);
        }
    });
}

function loadStatusMap2() {
    var statusCareId = $("#ddlStatusCare").val();
    var url = "/admin/api/statusmap/GetByStatusCareId?statusCareId=" + statusCareId;
    $.ajax({
        url: url,
        type: 'GET',
        success: function (data) {
            if (data == null) return;
            var options = '';
            for (var i = 0; i < data.length; i++) {
                options += data[i].LevelIdNext > 0 
                    ? '<option value="' + data[i].Id + '">' + data[i].Name + ' - L' + data[i].LevelIdNext + '</option>'
                    : '<option value="' + data[i].Id + '">' + data[i].Name + '</option>';
            }
            $('#ddlStatusMap').empty().append(options);
        }
    });
}

//load user theo mot nhom
function loadUserByGroupId() {      
    var groupId = $('#GroupId').val();
    if (groupId === undefined) return null;   
    var url = "/admin/api/user/GetByGroupId?groupId=" + groupId;
    return $.ajax({
        url: url,
        type: 'GET',
        success: function (data) {
            if (data == null) return;
            
            var options = '';
            if (data.length > 1)
                options += '<option value="0">Tất cả</option>';
            for (var i = 0; i < data.length; i++) {
                options += '<option value="' + data[i].UserID + '">' + data[i].FullName + '</option>';
            }
            $('#UserId').empty().append(options);
        }
    });
}
//Load user cho bao cao ty le cac level
function loadUserByGroupId_BC300() {
    var groupId = $('#chkGroups').is(':checked') ? 0 : buildParamGroups();
    if (groupId === undefined) return null;
    var url = "/admin/api/user/GetByGroupId?groupId=" + groupId;
    return $.ajax({
        url: url,
        type: 'GET',
        success: function (data) {
            if (data == null) return;

            var options = '';
            //if (data.length > 1)
            options += '<option value="0">Tất cả</option>';
            for (var i = 0; i < data.length; i++) {
                options += '<option value="' + data[i].UserID + '">' + data[i].FullName + '</option>';
            }
            $('#UserId').empty().append(options);
        }
    });
}

function GetConsultantNomrByBranchId(all) {
    var branchId = $('#BranchId').val();
    if (branchId === undefined) branchId = $('#dropUserBranch').val();
    if (branchId === undefined) return null;

    var url = "/admin/api/user/GetConsultantNomrByBranchId?branchId=" + branchId;
    return $.ajax({
        url: url,
        type: 'GET',
        success: function (data) {
            if (data == null) return;

            var options = '';
            if (all !== undefined && data.length > 1)
                options += '<option value="0">Tất cả</option>';
            for (var i = 0; i < data.length; i++) {
                options += '<option value="' + data[i].ConsultantId + '">' + data[i].FullName + '</option>';
            }
            $('#UserId').empty().append(options);
            $('#ConsultantId').empty().append(options);
        }
    });
}

function standardDatetime(datetime) {
    // DateTime
    var time;
    var value = Math.abs(new Date() - new Date(datetime));
    value = Math.round(value / 1000);
    if (value < 60)
        time = '<b>' + value + '</b>' + ' giây';
    else {
        value = Math.round(value / 60);
        if (value < 60)
            time = '<b>' + value + '</b>' + ' phút';
        else {
            value = Math.round(value / 60);
            if (value < 24)
                time = '<b>' + value + '</b>' + ' giờ';
            else {
                value = Math.round(value / 24);
                time = '<b>' + value + '</b>' + ' ngày';
            }
        }
    }
    return time + ' trước';
}

function validateEmail(value) {
    if (value == null || value.length == 0) return true;
    var regex = /^([\w\.\-]+)@([\w\-]+)((\.(\w){2,10})+)$/;
    return value.match(regex);
}

function validateMobile(value, isValid) {
    var PREFIX_PHONE = [
        "86",
        "96",
        "97",
        "98",
        "32",
        "33",
        "34",
        "35",
        "36",
        "37",
        "38",
        "39",
        "90",
        "93",
        "70",
        "79",
        "77",
        "76",
        "78",
        "91",
        "83",
        "84",
        "85",
        "81",
        "82",
        "92",
        "56",
        "58",
        "99",
        "59",
    ]
    if (value == null || value.length == 0) return true;
    // Check đầu số các thuê bao di động trong nước
    var prefix = value.substr(0, 2);
    if (PREFIX_PHONE.indexOf(prefix) > -1 && isValid) {
        var regex = /^(?!0)((\d{3}-\d{3}-\d{4})|\d{9})$/;
        return value.match(regex);
    } else {
        // Không phải thuê bao di động trong nước
        var regex = /^\d+$/;
        return value.match(regex);
    }
}

// dd/MM/yyyy HH:mm:ss
function validateDateTime(dt) {
    try {
        var isValidDate = false;
        var arr1 = dt.split('/');
        var year; var month; var day; var hour; var minute; var sec;
        if (arr1.length == 3) {
            var arr2 = arr1[2].split(' ');
            if (arr2.length == 2) {
                var arr3 = arr2[1].split(':');
                try {
                    year = parseInt(arr2[0], 10); month = parseInt(arr1[1], 10); day = parseInt(arr1[0], 10);
                    hour = parseInt(arr3[0], 10); minute = parseInt(arr3[1], 10); sec = parseInt(arr3[0], 10);

                    // Validate
                    if (year <= 0) return false;
                    if (day <= 0 || day > 32) return false;
                    if (sec < 0 || sec > 60) return false;
                    if (hour < 0 || hour > 24) return false;
                    if (month <= 0 || month > 12) return false;
                    if (minute < 0 || minute > 60) return false;
                    
                    var isValidTime = false;
                    if (hour >= 0 && hour <= 23 && minute >= 0 && minute <= 59 && sec >= 0 && sec <= 59)
                        isValidTime = true;
                    else if (hour == 24 && minute == 0 && sec == 0)
                        isValidTime = true;

                    if (isValidTime) {
                        var isLeapYear = false;
                        if (year % 4 == 0) isLeapYear = true;

                        if ((month == 4 || month == 6 || month == 9 || month == 11) && (day >= 0 && day <= 30))
                            isValidDate = true;
                        else if ((month != 2) && (day >= 0 && day <= 31))
                            isValidDate = true;

                        if (!isValidDate) {
                            if (isLeapYear) {
                                if (month == 2 && (day >= 0 && day <= 29))
                                    isValidDate = true;
                            }
                            else {
                                if (month == 2 && (day >= 0 && day <= 28))
                                    isValidDate = true;
                            }
                        }
                    }
                }
                catch (er) {
                    isValidDate = false;
                }
            }

        }

        return isValidDate;
    }
    catch (err) {
        return false;
    }
}

// dd/MM/yyyy HH:mm:ss
function convertToDateTime(dt) {
    if (validateDateTime(dt)) {
        var arr1 = dt.split('/');
        if (arr1.length == 3) {
            var arr2 = arr1[2].split(' ');
            if (arr2.length == 2) {
                var arr3 = arr2[1].split(':');
                var year = parseInt(arr2[0], 10);
                var month = parseInt(arr1[1], 10);
                var day = parseInt(arr1[0], 10);
                var hour = parseInt(arr3[0], 10);
                var minute = parseInt(arr3[1], 10);
                var sec = parseInt(arr3[0], 10);
                return new Date(year, month, day, hour, minute, sec, 0);
            }
        }
    }
    return null;
}

function openDialog(id) {
    console.log('thnagh==');
    var top = 50;
    var left = (screen.width / 2) - (1200 / 2);
    window.open("/admin/contactlevelinfo/Edit/" + id, '_blank', "width=1200,height=600,modal=yes,alwaysRaised=yes,top=" + top + ",left=" + left, null);
}

function openDialogHandoverAdvisor(id) {
    var top = 50;
    var left = (screen.width / 2) - (1200 / 2);
    window.open("/admin/handoveradvisor/HandoverContact/" + id, null, "width=1200,height=600,modal=yes,alwaysRaised=yes,top=" + top + ",left=" + left, null);
}

function BanGiao(id, code, phone, email, fullname, feePackageType, note, level_crm, deposit_need, actually_paid, technical_test, tvts_id, tvts_name, transaction_reason, sb100) {
    alert(id + phone + email + fullname + feePackageType);
    var url = "/admin/api/AdvisorsCreate/CreateContacts"
    $.ajax({
        url: url,
        type: 'POST',            
        data: {
            code: id,
            phone: phone,
            email: email,
            fullname: fullname,
            package_want_to_learn: feePackageType,
            note: note,
            level_crm: level_crm,
            deposit_need: deposit_need,
            actually_paid: actually_paid,
            technical_test: technical_test,
            tvts_id: tvts_id,
            tvts_name: tvts_name,
            transaction_reason: transaction_reason,
            sb100: sb100
        },
        success: function (data) {
            alertSuccess("Bàn giao thành công");
        },
        error: function (data) {
            alertError("Có lỗi xảy ra trong quá trình bàn giao, vui lòng thực hiện lại!");
        }
    });
}

function openDialogCollaborator(id) {
    var params = [
        'height=' + screen.height,
        'width=' + screen.width,
        'fullscreen=yes',
        'scrollbars=yes'
    ].join(',');
    window.open("/admin/contactlevelinfo/EditCollaborator/" + id, '', params);
}

function openDialogL3(id, appointmentId, phoneNumber) {
    var params = [
        'height=' + screen.height,
        'width=' + screen.width,
        'fullscreen=yes',
        'scrollbars=yes'
    ].join(',');
    window.open('/admin/contactlevelinfo/editl3/' + id + '?appointmentId=' + appointmentId + '&phoneNumber=' + phoneNumber, '', params);
}

function getCellById(grid_selector, id) {
    var allRowsInGrid = jQuery(grid_selector).jqGrid('getRowData');
    for (i = 0; i < allRowsInGrid.length; i++) {
        if (id == allRowsInGrid[i].Id) {
            return allRowsInGrid[i].Name;
        }
    }
    return '';
}

function loadGroup(branchId, employeeTypeId, control) {
    var url = "/admin/api/group/GetGroupByBranchId";
    return $.ajax({
        url: url,
        type: 'GET',
        data: {
            branchId: branchId,
            employeeTypeId: employeeTypeId
        },
        success: function (data) {
            var options = '<option value="0">Tất cả</option>';
            if (data != null) {
                for (var i = 0; i < data.length; i++) {
                    options += '<option value="' + data[i].GroupId + '">' + data[i].Name + '</option>';
                }
            }
            $('#' + control).empty().append(options);
        }
    });
}

function loadUser(branchId, groupId, employeeTypeId, control) {
    return $.ajax({
        type: 'GET',
        url: '/admin/api/user/GetNormsByBranchId',
        data: {
            groupId: groupId,
            branchId: branchId,
            employeeTypeId: employeeTypeId
        },
        success: function (data) {
            var options = '<option value="0">Tất cả</option>';
            if (data != null) {
                for (var i = 0; i < data.length; i++) {
                    options += '<option value="' + data[i].UserID + '">' + data[i].FullName + '</option>';
                }
            }
            $('#' + control).empty().append(options);
            $('#' + control).val('0').trigger("liszt:updated");
        }
    });
}
//Lay cac goi hoc niem yet
function loadPackageListed(control) {
    return $.ajax({
        type: 'GET',
        url: '/admin/api/user/GetNormsByBranchId',
        data: {
            groupId: groupId,
            branchId: branchId,
            employeeTypeId: employeeTypeId
        },
        success: function (data) {
            var options = '';
            if (data != null) {
                for (var i = 0; i < data.length; i++) {
                    options += '<option value="' + data[i].UserID + '">' + data[i].FullName + '</option>';
                }
            }
            $('#' + control).empty().append(options);
            $('#' + control).val('0').trigger("liszt:updated");
        }
    });
}

function getIds(control) {
    var value = $('#' + control).val();
    if (value == 0) {
        value = '';
        var elements = $('#' + control + ' option');
        for (var i = 0; i < elements.length; i++) {
            var elmValue = $(elements[i]).val();
            if (elmValue == 0) continue;
            value += value.length == 0 ? elmValue : ',' + elmValue;
        }
    }
    return value;
}

function checkDuplicate() {

    //Check nguon trong phan them contact 
    var nguon = $('#ContactInfo_TypeId').val();
    if (nguon == 0) {
        alertError('Bạn chưa chọn nguồn, vui lòng chọn lại!');
        return false;
    }

    // Check mobile
    var mobile1 = $('#ContactInfo_Mobile1').val();
    if (mobile1 !== undefined) {
        if (mobile1.length > 0 && mobile1.indexOf(0) == 0) {
            mobile1 = mobile1.substr(1);
            $('#ContactInfo_Mobile1').val(mobile1);
        }
        if (!validateMobile(mobile1, true)) {
            alertError('Số điện thoại 1 không hợp lệ, vui lòng thử lại');
            return false;
        }
    }

    var mobile2 = $('#ContactInfo_Mobile2').val();
    if (mobile2 !== undefined) {
        if (mobile2.length > 0 && mobile2.indexOf(0) == 0) {
            mobile2 = mobile2.substr(1);
            $('#ContactInfo_Mobile2').val(mobile2);
        }
        if (!validateMobile(mobile2, false)) {
            alertError('Số điện thoại 2 không hợp lệ, vui lòng thử lại');
            return false;
        }
    }

    var mobile3 = $('#ContactInfo_Mobile3').val();
    if (mobile3 !== undefined) {
        if (mobile3.length > 0 && mobile3.indexOf(0) == 0) {
            mobile3 = mobile3.substr(1);
            $('#ContactInfo_Mobile3').val(mobile3);
        }
        if (!validateMobile(mobile3, true)) {
            alertError('Số điện thoại 3 không hợp lệ, vui lòng thử lại');
            return false;
        }
    }

    if (mobile1 !== undefined && mobile2 !== undefined) {
        if (mobile1 != '' && mobile2 != '' && mobile1 == mobile2) {
            alertError('Số điện thoại 1 và 2 đang bị trùng nhau, vui lòng thử lại');
            return false;
        }
    }

    if (mobile1 !== undefined && mobile3 !== undefined) {
        if (mobile1 != '' && mobile3 != '' && mobile1 == mobile3) {
            alertError('Số điện thoại 1 và 3 đang bị trùng nhau, vui lòng thử lại');
            return false;
        }
    }

    if (mobile2 !== undefined && mobile3 !== undefined) {
        if (mobile2 != '' && mobile3 != '' && mobile2 == mobile3) {
            alertError('Số điện thoại 2 và 3 đang bị trùng nhau, vui lòng thử lại');
            return false;
        }
    }

    if (mobile1 === '' && mobile2 === '') {
        alertError('Số điện thoại 1 hoặc 2 không được trống, vui lòng thử lại');
        return false;
    }

    // Check email
    var email1 = $('#ContactInfo_Email').val();
    if (email1 !== undefined) {
        if (!validateEmail(email1)) {
            alertError('Email 1 không hợp lệ, vui lòng thử lại');
            return false;
        }
    }

    var email2 = $('#ContactInfo_Email2').val();
    if (email2 !== undefined) {
        if (!validateEmail(email2)) {
            alertError('Email 2 không hợp lệ, vui lòng thử lại');
            return false;
        }
    }

    if (email1 !== undefined && email2 !== undefined) {
        if (email1 != '' && email2 != '' && email1 == email2) {
            alertError('Email 1 và 2 đang bị trùng nhau, vui lòng thử lại');
            return false;
        }
    }
    return true;
}