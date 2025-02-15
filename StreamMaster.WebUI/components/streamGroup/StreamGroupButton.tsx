import { useSelectedItems } from '@lib/redux/hooks/selectedItems';

import SMPopUp from '@components/sm/SMPopUp';
import { useSelectedStreamGroup } from '@lib/redux/hooks/selectedStreamGroup';
import { memo, useMemo } from 'react';
import StreamGroupProfileButton from '../profiles/StreamGroupProfileButton';
import StreamGroupCreateDialog from './StreamGroupCreateDialog';
import StreamGroupDataSelector from './StreamGroupDataSelector';
import { StreamGroupSelector } from './StreamGroupSelector';

interface StreamGroupButtonProperties {
  className?: string;
}

const StreamGroupButton = ({ className = 'sm-w-10rem sm-input-dark' }: StreamGroupButtonProperties) => {
  const { setSelectedItems } = useSelectedItems('selectedStreamGroup');
  const { selectedStreamGroup, setSelectedStreamGroup } = useSelectedStreamGroup('StreamGroup');

  const headerTemplate = useMemo(() => {
    return (
      <>
        <StreamGroupProfileButton />
        <StreamGroupCreateDialog />
      </>
    );
  }, []);

  return (
    <div className="flex justify-content-center align-items-center">
      <div className={className}>
        <StreamGroupSelector
          onChange={(sg) => {
            setSelectedStreamGroup(sg);
            setSelectedItems([sg]);
          }}
          selectedStreamGroup={selectedStreamGroup}
          zIndex={11}
        />
      </div>
      <div className="pr-1" />
        <SMPopUp
          buttonClassName="sm-w-8rem icon-sg"
          buttonLabel="Stream Groups"
          contentWidthSize="6"
          header={headerTemplate}
          icon="pi-list-check"
          modal
          iconFilled
          title="Stream Groups"
        >
          <StreamGroupDataSelector id={'StreamGroup'} />
        </SMPopUp>
    </div>
  );
};

StreamGroupButton.displayName = 'StreamGroupButton';

export interface M3UFilesEditorProperties {}

export default memo(StreamGroupButton);
