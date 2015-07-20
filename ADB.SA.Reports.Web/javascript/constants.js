var arrangement = [];
arrangement[0] = 'agendafilter';
arrangement[1] = 'policyfilter';
arrangement[2] = 'rulefilter';
arrangement[3] = 'processfilter';
arrangement[4] = 'subprocessfilter';
arrangement[5] = 'applicationfilter';
arrangement[6] = 'modulefilter';

var directionlist = [];
directionlist['agendafilter'] = 'ltr';
directionlist['policyfilter'] = 'ltr';
directionlist['rulefilter'] = 'ltr';
directionlist['processfilter'] = 'rtl';
directionlist['subprocessfilter'] = 'rtl';
directionlist['applicationfilter'] = 'rtl';
directionlist['modulefilter'] = 'rtl';

var toggleListLTR = [];
toggleListLTR['agendafilter'] = [2, 3, 4, 5, 6];
toggleListLTR['policyfilter'] = [3, 4, 5, 6];
toggleListLTR['rulefilter'] = [4, 5, 6];
toggleListLTR['processfilter'] = [6];
toggleListLTR['subprocessfilter'] = [6];
toggleListLTR['applicationfilter'] = [];
toggleListLTR['modulefilter'] = [];

var toggleListRTL = [];
toggleListRTL['agendafilter'] = [];
toggleListRTL['policyfilter'] = [];
toggleListRTL['rulefilter'] = [0];
toggleListRTL['processfilter'] = [0, 1];
toggleListRTL['subprocessfilter'] = [0, 1, 2];
toggleListRTL['applicationfilter'] = [0, 1, 2];
toggleListRTL['modulefilter'] = [0, 1, 2, 3, 5];


//Which dropdowns to be reset
var resetListLTR = [];
resetListLTR['agendafilter'] = [1, 2, 3, 4, 5, 6];
resetListLTR['policyfilter'] = [2, 3, 4, 5, 6];
resetListLTR['rulefilter'] = [3, 4, 5, 6];
resetListLTR['processfilter'] = [4,5, 6];
resetListLTR['subprocessfilter'] = [6];
resetListLTR['applicationfilter'] = [6];
resetListLTR['modulefilter'] = [];

var resetListRTL = [];
resetListRTL['agendafilter'] = [];
resetListRTL['policyfilter'] = [0];
resetListRTL['rulefilter'] = [0, 1];
resetListRTL['processfilter'] = [0, 1, 2];
resetListRTL['subprocessfilter'] = [0, 1, 2, 3];
resetListRTL['applicationfilter'] = [0, 1, 2, 3, 4, 5];
resetListRTL['modulefilter'] = [0, 1, 2, 3, 4, 5];

//See what is the next item
var nextListLTR = [];
nextListLTR['agendafilter'] = ['policyfilter'];
nextListLTR['policyfilter'] = ['rulefilter'];
nextListLTR['rulefilter'] = ['processfilter'];
nextListLTR['processfilter'] = ['subprocessfilter', 'applicationfilter'];
nextListLTR['subprocessfilter'] = ['modulefilter'];
nextListLTR['applicationfilter'] = [''];
nextListLTR['modulefilter'] = '';

var nextListRTL = [];
nextListRTL['agendafilter'] = [''];
nextListRTL['policyfilter'] = ['agendafilter'];
nextListRTL['rulefilter'] = ['policyfilter'];
nextListRTL['processfilter'] = ['rulefilter'];
nextListRTL['subprocessfilter'] = ['processfilter'];
nextListRTL['applicationfilter'] = ['subprocessfilter'];
nextListRTL['modulefilter'] = ['subprocessfilter'];