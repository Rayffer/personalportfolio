import * as vm from './list.vm';
import * as apivm from './list.api.model';

export const mapMemberFromApiToVm = (member: apivm.MemberEntityApi) : vm.MemberEntity => ({
    id: member.id,
    login: member.login,
    avatar_url: member.avatar_url,
});

export const mapMemberCollectionFromApiToVm = (memberCollection: apivm.MemberEntityApi[]): vm.MemberEntity[] =>
    memberCollection.map((member) => mapMemberFromApiToVm(member));